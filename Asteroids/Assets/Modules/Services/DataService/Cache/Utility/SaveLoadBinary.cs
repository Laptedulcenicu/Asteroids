using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Modules.Services.DataService
{
    public class SaveLoadBinary : ISaveLoadUtility
    {
        public void Save<T>(string path, T data) where T : IPersistentModel
        {
            var task = Task.Run(() =>
            {
                using (Stream ms = File.OpenWrite(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, data);
                    ms.Flush();
                    ms.Close();
                    ms.Dispose();
                }
            });
        }

        public T Load<T>(string path) where T : IPersistentModel
        {
            object data = null;

            var loadTask = Task.Run(() =>
            {
                using (FileStream fs = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    data = formatter.Deserialize(fs);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
            });

            loadTask.Wait();
            return (T)data;
        }
    }
}