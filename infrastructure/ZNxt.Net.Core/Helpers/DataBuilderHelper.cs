using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Consts;

namespace ZNxt.Net.Core.Helpers
{
    public class DataBuilderHelper
    {
        private const string DATA_KEY = "data";
        private const string CODE_KEY = "code";
        private const string MESSAGE_KEY = "message";

        public DataBuilderHelper AddDataToArray(JObject data, JObject addedValue)
        {
            if (data[DATA_KEY] == null)
            {
                data.Add(DATA_KEY, new JArray());
            }
            (data[DATA_KEY] as JArray).Add(addedValue);
            return this;
        }

        public JObject GetResponseObject(int code, string message = null)
        {
            var data = new JObject();
            data[CODE_KEY] = code;
            if (!string.IsNullOrEmpty(message))
            {
                data[MESSAGE_KEY] = message;
            }
            else
            {
                data[MESSAGE_KEY] = CommonConst.Messages[code];
            }
            return data;
        }

        public DataBuilderHelper AddData(JObject data, string key, string value)
        {
            data[key] = value;
            return this;
        }

        public DataBuilderHelper AddData(JObject data, string key, JToken value)
        {
            data[key] = value;
            return this;
        }
    }
}