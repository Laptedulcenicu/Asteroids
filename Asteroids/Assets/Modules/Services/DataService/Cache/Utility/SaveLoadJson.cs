using System;
using System.IO;
using System.Threading.Tasks;

namespace Modules.Services.DataService
{
    public class SaveLoadJson : ISaveLoadUtility
    {
        private const string k_Extension = ".json";

        private readonly ISerializer _serializer = new JsonSerializer();

        public void Save<T>(string path, T data) where T : IPersistentModel
        {
            Task.Run(() =>
            {
                var saveData = _serializer.Serialize(data);

                File.WriteAllText($"{path}{k_Extension}", Convert.ToString(saveData));
            });
        }

        public T Load<T>(string path) where T : IPersistentModel
        {
            string filePath = $"{path}{k_Extension}";

            if (!File.Exists(filePath))
                return default;

            T data = default;

            var loadTask = Task.Run(() =>
            {
                string json = File.ReadAllText(filePath);

                data = _serializer.Deserialize<T>(json);
            });

            loadTask.Wait();
            return data;
        }
    }
}