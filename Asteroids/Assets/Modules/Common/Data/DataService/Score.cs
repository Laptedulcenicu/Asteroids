using System;
using Newtonsoft.Json;
using SW.Modules.Services.PersistenceService;

namespace Modules.Common
{
    [Serializable]
    public class Score: ISaveModel
    {
        [JsonIgnore] private string _key = "score";

        [JsonIgnore] public string Key => _key;

        public int CurrentScore { get; set; } = 0;
    }
}