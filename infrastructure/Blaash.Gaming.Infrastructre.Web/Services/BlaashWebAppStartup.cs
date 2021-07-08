using Blaash.Gaming.Infrastructure.Database.Mongo;
using Blaash.Gaming.Infrastructure.Database.PostgresSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Web.Handlers;
using ZNxt.Net.Core.Web.Services;

namespace Blaash.Gaming.Infrastructre.Web.Services
{
    public abstract class BlaashWebAppStartup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public BlaashWebAppStartup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddZNxtSSO(Environment);
            services.AddZNxtApp();
            var serviceProvider = services.BuildServiceProvider();
            var routing = serviceProvider.GetService<IRouting>();
            LoadModules(routing);
            routing.ReLoadRoutes();
            MVCServiceExtention.InitRoutingDepedencies(services, serviceProvider);
            services.AddZNxtBearerAuthentication();
            services.AddZNxtIdentityServer();
           
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<ILogReader, ConsoleLogger>();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(fordwardedHeaderOptions);
            var corurl = CommonUtility.GetAppConfigValue("cor_urls");
            if (string.IsNullOrEmpty(corurl))
            {
                corurl = "http://localhost:50071";
            }
            
            app.UseCors(
                    options => options.WithOrigins(corurl.Split(';')).AllowAnyMethod()
                    .AllowAnyHeader().AllowCredentials()
             );
            var ssourl = CommonUtility.GetAppConfigValue(CommonConst.CommonValue.SSOURL_CONFIG_KEY);

            app.UseHttpProxyHandler();
            if (!string.IsNullOrEmpty(ssourl))
            {
                app.UseAuthentication();
            }
            app.UseRouting();
            if (!string.IsNullOrEmpty(ssourl))
            {
                app.UseAuthorization();

            }
            app.Map("/api", HandlerAPI);
            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action}/{id?}"));


           // app.MapWhen(context => true, HandlerStaticContant);

        }

        public virtual  void HandlerAPI(IApplicationBuilder app)
        {
            app.UseApiHandler();

        }
        public virtual void HandlerStaticContant(IApplicationBuilder app)
        {
            app.UseStaticContentHandler();

        }
        public abstract void LoadModules(IRouting routing);
       
    }
}
