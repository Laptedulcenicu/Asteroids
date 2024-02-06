using System;

namespace Modules.Gameplay
{
    public class GameLoopEvents
    {
        public Action OnFail;
        public Action OnWin;
        public Action OnScoreIncrease;
        public Action OnMoveObstacles;
        public Action OnDestroyObstacle;
    }
}