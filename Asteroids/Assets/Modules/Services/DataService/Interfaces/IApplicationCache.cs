using System.Collections.Generic;
using SW.Modules.Services.PersistenceService;

namespace Modules.Services.DataService
{
    public interface IApplicationCache
    {
        void Init();
        void SavePersistent(IDictionary<string, ISaveModel> data);
        ISaveModel LoadPersistent(string key);
    }
}