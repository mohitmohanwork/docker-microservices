using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blaash.Gaming.Infrastructre.Common.Serializer
{
    public static class JsonSerializer
    {
        public static T Deserialize<T>(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            else return default(T);
        }

        public static T Deserialize<T>(JObject data)
        {
            if (data != null)
            {
                return Deserialize<T>(data.ToString());
            }
            else return default(T);
             
        }

        public static JObject Serialize<T>(T data)
        {
            string varData = JsonConvert.SerializeObject(data);
            return JObject.Parse(varData);
        }
    }
}
