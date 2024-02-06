using System.Collections.Generic;
using System.IO;
using SW.Modules.Services.PersistenceService;

namespace Modules.Services.DataService
{
    public class ApplicationCache : IApplicationCache
    {
        private readonly ISaveLoadUtility _saveLoadUtility = new SaveLoadJson();
        private IDictionary<string, ISaveModel> _gameCacheData;

        public void Init()
        {
            if (!Directory.Exists(CacheConfig.CachePath))
                Directory.CreateDirectory(CacheConfig.CachePath);
            if (!Directory.Exists(CacheConfig.PersistentDataFolder))
                Directory.CreateDirectory(CacheConfig.PersistentDataFolder);

            var data = _saveLoadUtility.Load<IPersistentModel>(CacheConfig.PersistentDataPath);
            _gameCacheData = data != null ? data.PersistentData : new Dictionary<string, ISaveModel>();
        }

        public void SavePersistent(IDictionary<string, ISaveModel> data)
        {
            SaveGameCacheData(data);
            _saveLoadUtility.Save(CacheConfig.PersistentDataPath, new PersistentModel(_gameCacheData));
        }

        private void SaveGameCacheData(IDictionary<string, ISaveModel> environmentModels)
        {
            foreach (var element in environmentModels)
            {
                _gameCacheData[element.Key] = element.Value;
            }
        }

        public ISaveModel LoadPersistent(string key)
        {
            var loadedData = _gameCacheData.TryGetValue(key, out var data) ? data : null;

            return loadedData;
        }
    }
}