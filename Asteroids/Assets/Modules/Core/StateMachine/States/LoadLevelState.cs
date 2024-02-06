using Modules.Common;
using Modules.Core.Scripts.Utilities;
using Modules.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Core
{
    public class LoadLevelState : IState
    {
        private const string k_SceneName = "Lv 1";

        private readonly IUIFactory _uiFactory;
        private readonly ISceneFactory _sceneFactory;
        private readonly ISceneTransitionService _transitionService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressData _progressData;

        public LoadLevelState(ISceneFactory sceneFactory, IUIFactory uiFactory,
            ISceneTransitionService sceneTransitionService, GameStateMachine gameStateMachine,
            IProgressData progressData)
        {
            _uiFactory = uiFactory;
            _sceneFactory = sceneFactory;
            _transitionService = sceneTransitionService;
            _gameStateMachine = gameStateMachine;
            _progressData = progressData;
        }


        public void Enter()
        {
            _transitionService.ChangeScene(k_SceneName, SceneLoaded);
        }

        private void SceneLoaded()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneController = scene.GetComponent<GameplaySceneController>();

            InitScene(sceneController);
            InitUI(sceneController);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitUI(GameplaySceneController sceneController)
        {
            var ui = _uiFactory.CreateUI();
            sceneController.UIController = ui.GetComponent<IUIController>();
        }

        private void InitScene(GameplaySceneController sceneController)
        {
            InitPlayer(sceneController);
        }

        private void InitPlayer(GameplaySceneController sceneController)
        {
            var player = _sceneFactory.CreatePlayer();
            sceneController.Player = player.GetComponent<PlayerView>();
            InitObject(player.transform, sceneController.PlayerSpawnPoint);
        }

        private void InitObject(Transform currentObject, Transform marker)
        {
            currentObject.position = marker.position;
            currentObject.eulerAngles = marker.eulerAngles;
            currentObject.transform.SetParent(marker);
        }
    }
}