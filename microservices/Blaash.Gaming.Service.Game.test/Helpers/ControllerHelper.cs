//using Blaash.Gaming.Service.Game.Api;
//using Blaash.Gaming.Service.Game.Helpers;
//using Blaash.Gaming.Service.Game.Services.Api.Game;
//using Newtonsoft.Json.Linq;
//using System.Collections.Generic;

//namespace Blaash.Gaming.Service.Game.test.Helpers
//{
//    public static class ControllerHelper
//    {
//        public static PingController GetPingController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
//        {
//            var logger = new LoggerMock();
//            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
//            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
//            return new PingController(httpProxy, CommonExtensions.GetDBService(httpProxy), CommonExtensions.GetRDBService(httpProxy), logger, responseBuilder);
//        }

//        public static GameController GetGameController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
//        {
//            var logger = new LoggerMock();
//            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
//            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
//            var rdp = CommonExtensions.GetRDBService(httpProxy);
//            rdp.Init("NPGSQL", "Host=103.212.120.203;Username=root;Password=root;Database=gameplay");
//            return new GameController(httpProxy, CommonExtensions.GetDBService(httpProxy), rdp, logger, responseBuilder);
//        }


//    }
//}
