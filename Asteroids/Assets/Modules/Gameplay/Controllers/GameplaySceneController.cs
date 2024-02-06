using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Common;
using UnityEngine;

namespace Modules.Gameplay
{
    public class GameplaySceneController : MonoBehaviour
    {
        [SerializeField] private Transform inputControllerParent;
        [SerializeField] private InteractableController interactableController;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform obstaclesParent;
        [SerializeField] private BottomWall bottomWall;
        [SerializeField] private DeathZone deathZone;
        private IInputController _inputController;
        public PlayerView Player { get; set; }
        public IUIController UIController { get; set; }
        public Transform PlayerSpawnPoint => playerSpawnPoint;
        public InteractableController InteractableController => interactableController;
        public List<ObstacleView> ObstaclesView { get; private set; } = new();

        public void Initialize(int maxBallCount, IInputSource inputSource, IAudioService audioService,
            ISceneTransitionService sceneTransitionService, GameLoopEvents gameLoopEvents)
        {
            SetInputSource(inputSource);
            InitializeInteractableController(inputSource);
            InitializeObstacles(audioService, gameLoopEvents.OnScoreIncrease, gameLoopEvents.OnDestroyObstacle);
            Player.Initialize(gameLoopEvents,audioService, maxBallCount);
            bottomWall.Initialize(Player.PlayerShoot,gameLoopEvents.OnMoveObstacles, maxBallCount);
            deathZone.Initialize(gameLoopEvents.OnFail);
            sceneTransitionService.FadeOut();
        }

        private void InitializeObstacles(IAudioService audioService, Action scoreIncrease, Action destroyObstacle)
        {
            ObstaclesView = obstaclesParent.GetComponentsInChildren<ObstacleView>().ToList();
            foreach (var obstacleView in ObstaclesView)
            {
                obstacleView.Initialize(audioService, scoreIncrease, destroyObstacle);
            }
        }

        private void SetInputSource(IInputSource inputSource)
        {
            _inputController = inputControllerParent.GetComponent<IInputController>();
            _inputController.Setup(inputSource);
        }

        private void InitializeInteractableController(IInputSource inputSource) =>
            interactableController.Initialize(inputSource, Player);
    }
}