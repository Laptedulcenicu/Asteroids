using Modules.Common;
using Modules.Services.AssetProviderService;
using Modules.Services.AudioService;
using Modules.Services.DataService;
using Modules.Services.FactoryService;
using Modules.Services.InputService;
using Modules.Services.LifecycleService;
using Modules.Services.SceneTransitionService;

namespace Modules.Core
{
    public class Bootstrapper
    {
        private GameStateMachine _gameStateMachine;
        private IInputService _inputService;
        private IDataService _dataService;
        private ILifecycleService _lifecycleService;
        private IAudioService _audioService;
        private ISceneTransitionService _sceneTransitionService;
        private IAssetProviderService _assetProviderService;
        private IFactoryService _factoryService;

        public void Initialize()
        {
            InitializeServices();

            _gameStateMachine = new GameStateMachine();
            AddStates();

            _audioService.PlayMusic();
            _gameStateMachine.Enter<LoadProgressDataState>();
        }

        private void AddStates()
        {
            var loadProgressDataState = new LoadProgressDataState(_dataService.ProgressData,
                _dataService.ApplicationCache, _lifecycleService, _gameStateMachine);
            var loadLevelState = new LoadLevelState(_factoryService.SceneFactory,
                _factoryService.UIFactory, _sceneTransitionService, _gameStateMachine, _dataService.ProgressData);
            var gameLoopState = new GameLoopState(_dataService.ProgressData, _inputService.InputSource,
                _audioService, _sceneTransitionService, _gameStateMachine);

            _gameStateMachine.States.Add(typeof(LoadProgressDataState), loadProgressDataState);
            _gameStateMachine.States.Add(typeof(LoadLevelState), loadLevelState);
            _gameStateMachine.States.Add(typeof(GameLoopState), gameLoopState);
        }

        private void InitializeServices()
        {
            _assetProviderService = new AssetProviderService();
            _factoryService = new FactoryService(_assetProviderService);
            _inputService = new InputService();
            _dataService = new DataService();
            _sceneTransitionService = new SceneTransitionService(_factoryService.ServiceFactory);
            _lifecycleService = new LifecycleService(_factoryService.ServiceFactory);
            _audioService = new AudioService(_factoryService.ServiceFactory);
        }
    }
}