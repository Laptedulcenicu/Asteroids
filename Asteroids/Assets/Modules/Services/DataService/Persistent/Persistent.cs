using System.Collections.Generic;
using Modules.Services.Common;
using SW.Modules.Services.PersistenceService;

namespace Modules.Services.DataService
{
    public class Persistent : IPersistent
    {
        private readonly IApplicationCache _applicationCache;

        internal Persistent(IApplicationCache applicationCache)
        {
            _applicationCache = applicationCache;
        }

        public ISaveModel GetPersistentData(string key) => _applicationCache.LoadPersistent(key);

        public void SavePersistantData(IDictionary<string, ISaveModel> environmentModels) =>
            _applicationCache.SavePersistent(environmentModels);
    }
}