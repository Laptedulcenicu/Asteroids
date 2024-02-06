using System;

namespace Modules.Common
{
    public interface IUIController
    {
        event Action OnPlay;
        event Action OnRestart;

        IMultiplayPanel MultiplayPanel { get; }
        ILeaderBoardPanel LeaderBoardPanel { get; }

        void Initialize(int level);
        void Play();
        void ActivateLosePanel();
        void ActivateWinPanel();
        void SetScoreText(int score);
        void OpenLeaderboard(int scoreCurrentScore);
    }
}