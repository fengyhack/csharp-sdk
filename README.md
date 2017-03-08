#Qiniu (Cloud) C# SDK (Modified)

[![Documentation](https://img.shields.io/badge/Qiniu%20C%23%20SDK-Documentation-brightgreen.svg)](https://developer.qiniu.com/kodo/sdk/csharp) [![Supported](https://img.shields.io/badge/Supported-.NET2.0%2B%2F.NETCore%2FUWP-brightgreen.svg)](#)

[![GitHub release](https://img.shields.io/github/release/qiniu/csharp-sdk.svg?label=github)](https://github.com/qiniu/csharp-sdk/releases) [![Github Downloads](https://img.shields.io/github/downloads/qiniu/csharp-sdk/total.svg?colorB=aaaaff)](https://github.com/qiniu/csharp-sdk/releases) [![NuGet release](https://img.shields.io/nuget/v/Qiniu.Shared.ExtJson.svg?colorB=aa77ff)](https://www.nuget.org/packages/Qiniu.Shared.ExtJson)  [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/qiniu/csharp-sdk/master/LICENSE)

此版本**移除了对于Json.NET的依赖**，我们称之为`ext-json`版本。

安装时，请选择`ext-json`版本，或者在NuGet中搜索`Qiniu.Shared.ExtJson`，又或者

```script
Install-Package Qiniu.Shared.ExtJson
```

在使用的时候**需要预先设置您的JSON序列化和反序列化器**。

可参阅examples示例，首先定义：

```csharp
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
```
然后设置它：

```csharp
// 使用前请先定义您的JSON序列化、反序列化方法
Qiniu.JSON.JsonHelper.JsonSerializer = new AnotherJsonSerializer();
Qiniu.JSON.JsonHelper.JsonDeserializer = new AnotherJsonDeserializer();
```

接下来的一切，与原始版本完全一致。