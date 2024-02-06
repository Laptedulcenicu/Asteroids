using Modules.Common;
using Modules.Services.DataService;

namespace Modules.Core
{
    public class LoadProgressDataState : IState
    {
        private readonly IApplicationCache _applicationCache;
        private readonly ILifecycleService _lifecycleService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressData _progressData;

        public LoadProgressDataState(IProgressData progressData, IApplicationCache applicationCache,
            ILifecycleService lifecycleService, GameStateMachine gameStateMachine)
        {
            _applicationCache = applicationCache;
            _lifecycleService = lifecycleService;
            _progressData = progressData;
            _gameStateMachine = gameStateMachine;
        }


        public void Enter()
        {
            _applicationCache.Init();
            _lifecycleService.AddDelegate(_progressData);
            _progressData.LoadProgress();
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}