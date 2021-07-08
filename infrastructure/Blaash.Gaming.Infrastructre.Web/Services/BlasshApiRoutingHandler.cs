using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
using ZNxt.Net.Core.Web.Handlers;
using ZNxt.Net.Core.Web.Services.SSO;

namespace Blaash.Gaming.Infrastructre.Web.Services
{
    public class BlasshApiRoutingHandler : ApiHandler
    {
        public BlasshApiRoutingHandler(RequestDelegate next, ILogger logger, IDBService dbService, IRouting routing, IHttpContextProxy httpContextProxy, IAssemblyLoader assemblyLoader, IServiceResolver serviceResolver, IResponseBuilder responseBuilder, IApiGatewayService apiGatewayService, IInMemoryCacheService inMemoryCacheService, IOAuthClientService oAuthClientService) : base(next, logger, dbService, routing, httpContextProxy, assemblyLoader, serviceResolver, responseBuilder, apiGatewayService, inMemoryCacheService, oAuthClientService)
        {
        }
        public override async Task<JObject> CallRemoteRouteCalls(HttpContext context, RoutingModel route)
        {
            var appToken = "route-call";
            var tenantid = string.Empty;
            var playerid = string.Empty;
            var user = _httpContextProxy.User;
            if (user != null)
            {
                var apiKeyClaim = user.claims.FirstOrDefault(f => f.Key == "api_key");
                if (apiKeyClaim != null)
                {
                    appToken = apiKeyClaim.Value;
                }
                var tnt = user.tenants.FirstOrDefault();
                if (tnt != null)
                {
                    tenantid = tnt.tenant_id;
                }
                playerid = user.user_id;
            }
            _logger.Debug(string.Format("Executing remote route:{0}:{1}", route.Method, route.Route));
            var headers = new Dictionary<string, string>();
            headers[CommonConst.CommonField.API_AUTH_TOKEN] = appToken;
            headers[CommonConst.CommonField.TENANT_ID] = tenantid;
            headers["player_id"] = user.user_id;
            var response = await _apiGatewayService.CallAsync(_httpContextProxy.GetHttpMethod(), _httpContextProxy.GetURIAbsolutePath(), _httpContextProxy.GetQueryString(), _httpContextProxy.GetRequestBody<JObject>(), headers);
            return response;
        }
    }
}
