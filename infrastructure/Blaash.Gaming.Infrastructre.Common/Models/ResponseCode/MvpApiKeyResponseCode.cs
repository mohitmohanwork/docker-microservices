using System.Collections.Generic;
using ZNxt.Net.Core.Interfaces;

namespace Blaash.Gaming.Infrastructre.Common
{
    public  class MvpApiKeyResponseCode : IMessageCodeContainer
    {
        public const int _ALREADY_EXISTS = 409;
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

        public MvpApiKeyResponseCode()
        {
            text[_ALREADY_EXISTS] = "ALREADY_EXISTS";
        }
    }
}
