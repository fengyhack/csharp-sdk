using System;
using Qiniu.Util;
using Qiniu.IO;
using Qiniu.IO.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Qiniu.JSON;

namespace CSharpSDKExamples
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string AK = "<ACCESS_KEY>";
			string SK = "<SECRET_KEY>";
			string bucket = "test";

			// Please set these before using JsonHelper
			JsonHelper.JsonSerializer = new NewtonsoftJsonSerializer();
			JsonHelper.JsonDeserializer = new NewtonsoftJsonDeserializer();

			// PutPolicy            
			PutPolicy putPolicy = new PutPolicy();
			putPolicy.Scope = bucket;
			putPolicy.SetExpires(3600);
			putPolicy.DeleteAfterDays = 1;

			string jstr = putPolicy.ToJsonString();

			// UploadToken
			Mac mac = new Mac(AK, SK);
			string token = Auth.CreateUploadToken(mac, jstr);

			string saveKey = "test_UploadManagerUploadFile_2.jpg";
			string localFile = "F:\\test.jpg";

			UploadManager um = new UploadManager();

			var result = um.UploadFile(localFile, saveKey, token);

			Console.WriteLine(result);

			Console.ReadLine();
		}
	}

	/// <summary>
	/// Json serializer.
	/// </summary>
	public class NewtonsoftJsonSerializer : IJsonSerializer
	{
		public string Serialize<T>(T obj) where T : new()
		{
			var settings = new JsonSerializerSettings();
			settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			settings.NullValueHandling = NullValueHandling.Ignore;
			return JsonConvert.SerializeObject(obj, settings);
		}
	}

	/// <summary>
	/// Json deserializer.
	/// </summary>
	public class NewtonsoftJsonDeserializer : IJsonDeserializer
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
