using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ZNxt.Net.Core.Interfaces
{
    [System.Obsolete]
    public interface ISMSService
    {
        bool Send(string toNumber, string text, bool putInQueue = true);

        bool Send(string toNumber, string smsTemplate, Dictionary<string, dynamic> data, bool putInQueue = true);

        bool Send(string toNumber, string smsTemplate, JObject data, bool putInQueue = true);
    }
}