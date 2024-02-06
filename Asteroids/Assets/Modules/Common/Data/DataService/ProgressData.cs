using System.Collections.Generic;
using Modules.Services.Common;
using SW.Modules.Services.PersistenceService;

namespace Modules.Common
{
    public class ProgressData : IProgressData
    {
        private readonly Level _level = new();
        private readonly Score _score = new();
        private readonly IPersistent _persistent;

        public ProgressData(IPersistent persistent)
        {
            _persistent = persistent;
        }

        public Level Level => _level;
        public Score Score => _score;

        public void OnApplicationQuit() => SaveProgress();

        public void OnApplicationFocus(bool hasFocus) => SaveProgress();

        public void OnApplicationPause(bool isPaused) => SaveProgress();

        public void SaveProgress()
        {
            var currentSavData = new Dictionary<string, ISaveModel>()
            {
                { _level.Key, _level },
                { _score.Key, _score }
            };

            _persistent.SavePersistantData(currentSavData);
        }

        public void LoadProgress()
        {
            var level = _persistent.GetPersistentData(_level.Key);
            var score = _persistent.GetPersistentData(_score.Key);
            if (level == null)
            {
                return;
            }

            _level.CurrentLevel = ((Level)level).CurrentLevel;
            _score.CurrentScore = ((Score)score).CurrentScore;
        }
    }
}