using Modules.Common;

namespace Modules.Gameplay
{
    public class ScoreCounter
    {
        private int _gameplayScore;

        public int GameplayScore => _gameplayScore;

        public ScoreCounter(Score score)
        {
            _gameplayScore = score.CurrentScore;
        }

        public void IncreaseScore()
        {
            _gameplayScore++;
        }

        public void Multiplay(int value)
        {
            _gameplayScore *= value;
        }
    }
}