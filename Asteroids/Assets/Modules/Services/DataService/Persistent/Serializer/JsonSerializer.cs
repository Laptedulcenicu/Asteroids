using System;
using Newtonsoft.Json;

namespace Modules.Services.DataService
{
    public class JsonSerializer : ISerializer
    {
        public object Serialize<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            return json;
        }

        public T Deserialize<T>(object data)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(data), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }
    }
}