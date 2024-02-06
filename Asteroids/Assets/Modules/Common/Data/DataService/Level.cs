using System;
using Newtonsoft.Json;
using SW.Modules.Services.PersistenceService;

namespace Modules.Common
{
    [Serializable]
    public class Level : ISaveModel
    {
        [JsonIgnore] private string _key = "level";

        [JsonIgnore] public string Key => _key;

        public int CurrentLevel { get; set; } = 1;
    }
}