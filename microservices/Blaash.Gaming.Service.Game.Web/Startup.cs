using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Consts;
using System;
using Microsoft.Extensions.Hosting;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Web.Services;
using ZNxt.Net.Core.Web.Handlers;
using Blaash.Gaming.Service.GamePlay;
using Blaash.Gaming.Infrastructre.Web.Services;

namespace Blaash.Gaming.Service.Game.Web
{
    public class Startup : BlaashWebAppStartup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration) : base(environment, configuration)
        {
            //var temp = GamePlayConstants.GetServiceInfo();
        }
        public override void LoadModules(IRouting routing)
        {
            Console.WriteLine("Loading modules .... ");
            routing.PushAssemblyRoute(typeof(GamePlayConstants).Assembly);

        }
    }
}
