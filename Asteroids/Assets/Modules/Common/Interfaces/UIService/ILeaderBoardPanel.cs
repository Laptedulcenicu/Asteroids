using System;

namespace Modules.Common
{
    public interface ILeaderBoardPanel
    {
        event Action OnNextLevel;
        event Action OnRestart;
    }
}