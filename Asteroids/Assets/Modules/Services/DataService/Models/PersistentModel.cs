using System;
using System.Collections.Generic;
using SW.Modules.Services.PersistenceService;

namespace Modules.Services.DataService
{
    [Serializable]
    public class PersistentModel : IPersistentModel
    {
        public IDictionary<string, ISaveModel> PersistentData { get; set; }
        public PersistentModel(IDictionary<string, ISaveModel> data)
        {
            PersistentData = data;
        }
    }
}