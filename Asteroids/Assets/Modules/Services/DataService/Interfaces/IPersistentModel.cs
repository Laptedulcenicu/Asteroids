using System.Collections.Generic;
using SW.Modules.Services.PersistenceService;

namespace Modules.Services.DataService
{
    public interface IPersistentModel
    {
        IDictionary<string, ISaveModel> PersistentData { get; set; }
    }
}