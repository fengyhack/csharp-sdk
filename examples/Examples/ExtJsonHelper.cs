using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Qiniu.JSON;

namespace CSharpSDKExamples
{
    public class AnotherJsonSerializer : IJsonSerializer
    {
        
        public string Serialize<T>(T obj) where T : new()
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(obj, settings);
        }
    }

    public class AnotherJsonDeserializer : IJsonDeserializer
    {
        public bool Deserialize<T>(string str, out T obj) where T : new()
        {
            obj = default(T);

            bool ok = true;

            try
            {
                obj = JsonConvert.DeserializeObject<T>(str);
            }
            catch (System.Exception)
            {
                ok = false;
            }

            return ok;
        }
    }
}
