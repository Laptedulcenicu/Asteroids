using System.Collections.Generic;
using SW.Modules.Services.PersistenceService;

namespace Modules.Services.Common
{
    public interface IPersistent
    {
        ISaveModel GetPersistentData(string key);

        void SavePersistantData(IDictionary<string, ISaveModel> environmentModels);
    }
}