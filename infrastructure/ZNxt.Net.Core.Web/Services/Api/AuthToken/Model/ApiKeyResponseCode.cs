using System.Collections.Generic;
using ZNxt.Net.Core.Interfaces;

namespace ZNxt.Net.Core.Web.Services.Api.AuthToken.Model
{
    public class ApiKeyResponseCode : IMessageCodeContainer
    {
        public const int _MAX_AUTH_TOKEN_REACHED = 9901;
        private Dictionary<int, string> text = new Dictionary<int, string>();

        public string Get(int code)
        {
            if (text.ContainsKey(code))
            {
                return text[code];
            }
            else
            {
                return string.Empty;
            }
        }

        public ApiKeyResponseCode()
        {
            text[_MAX_AUTH_TOKEN_REACHED] = "MAX_AUTH_TOKEN_REACHED";
        }
    }
}
