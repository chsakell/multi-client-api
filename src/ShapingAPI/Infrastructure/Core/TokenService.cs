using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ShapingAPI.Infrastructure.Core
{
    public class TokenService
    {
        public static JToken CreateJToken(object obj, string props)
        {
            string _serializedTracks = JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            JToken jtoken = JToken.Parse(_serializedTracks);
            if (!string.IsNullOrEmpty(props))
                Utils.FilterProperties(jtoken, props.ToLower().Split(',').ToList());

            return jtoken;
        }
    }
}
