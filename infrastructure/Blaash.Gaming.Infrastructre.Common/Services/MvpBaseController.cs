using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace Blaash.Gaming.Infrastructre.Common.Services
{
    public class MvpBaseController : ZNxt.Net.Core.Services.ApiBaseService
    {
        protected readonly IResponseBuilder _responseBuilder;
        protected IDBService _dBService;
        protected readonly IHttpContextProxy _httpContextProxy;
        protected readonly ILogger _logger;
        protected readonly IRDBService _rDBService;

        public MvpBaseController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger, IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, logger, responseBuilder)
        {
            _dBService = dBService;
            _httpContextProxy = httpContextProxy;
            _logger = logger;
            _responseBuilder = responseBuilder;
            _rDBService = rDBService;
        }

        protected T GetRequestBody<T>()
        {
            var request = _httpContextProxy.GetRequestBody<T>();
            return request;
        }
        protected void SetUser(BaseModelDbo model)
        {
            var user = _httpContextProxy.User;
            if (user != null)
            {
                model.created_by = user.user_id;
                model.updated_by = user.user_id;
            }
        }
        protected JObject ModelValidationFailResponse(Dictionary<string, string> results)
        {
            _logger.Debug("Model validation fail");
            JObject errors = new JObject();
            foreach (var error in results)
            {
                errors[error.Key] = error.Value;
            }
            return _responseBuilder.BadRequest(errors);
        }
        protected RequestPageData GetRequestPaggedData()
        {

            var currentPage = _httpContextProxy.GetQueryString("currentpage");
            int iCurrentPage = 1;
            if(!string.IsNullOrEmpty(currentPage))
                int.TryParse(currentPage, out iCurrentPage);

            var pagesize = _httpContextProxy.GetQueryString("pagesize");
            int ipagesize = 10;
            if (!string.IsNullOrEmpty(pagesize))
                int.TryParse(pagesize, out ipagesize);
            
            return new RequestPageData()
            {
                CurrentPage = iCurrentPage,
                PageSize = ipagesize,
                Skipped = (iCurrentPage - 1) * ipagesize
            };
        }
        protected JObject GetFiltersFromQueryString()
        {
            JObject filter = new JObject();
           var querystr =  _httpContextProxy.GetQueryString();

            if (!string.IsNullOrEmpty(querystr))
            {
                foreach (var q in querystr.Split('&'))
                {
                    var keyval = q.Split('=');
                    if (keyval.Length == 2)
                    {
                        if (keyval[0] != "currentpage" || keyval[0] != "pagesize")
                        {
                            filter[keyval[0]] = keyval[1];
                        }
                    }
                }

                
            }
            return filter;
        }
    }
}
