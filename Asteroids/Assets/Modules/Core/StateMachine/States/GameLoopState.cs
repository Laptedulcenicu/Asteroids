using Modules.Common;
using Modules.Core.Scripts.Utilities;
using Modules.Gameplay;
using UnityEngine.SceneManagement;

namespace Modules.Core
{
    public class GameLoopState : IState
    {
        private const int k_MaxBallCount = 50;
        private readonly IInputSource _inputSource;
        private readonly IAudioService _audioService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneTransitionService _sceneTransitionService;
        private readonly IProgressData _progressData;

        private GameLoopEvents _gameLoopEvents;
        private ScoreCounter _scoreCounter;
        private IUIController _UIController;

        public GameLoopState(IProgressData progressData, IInputSource inputSource, IAudioService audioService,
            ISceneTransitionService sceneTransitionService, GameStateMachine gameStateMachine)
        {
            _progressData = progressData;
            _inputSource = inputSource;
            _audioService = audioService;
            _gameStateMachine = gameStateMachine;
            _sceneTransitionService = sceneTransitionService;
        }

        private void RestartLevel()
        {
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Enter()
        {
            _gameLoopEvents = new GameLoopEvents();
            var scene = SceneManager.GetActiveScene();
            var sceneController = scene.GetComponent<GameplaySceneController>();
            _UIController = sceneController.UIController;
            _scoreCounter = new ScoreCounter(_progressData.Score);
            var obstaclesController = new ObstaclesController(sceneController, _gameLoopEvents);
            _gameLoopEvents.OnScoreIncrease += ScoreIncrease;
            InitializeGameplayUI(sceneController.InteractableController);
            sceneController.Initialize(k_MaxBallCount, _inputSource, _audioService, _sceneTransitionService,
                _gameLoopEvents);
        }

        private void InitializeGameplayUI(InteractableController interactableController)
        {
            _UIController.Initialize(_progressData.Level.CurrentLevel);
            _UIController.OnPlay += _UIController.Play;
            _UIController.OnRestart += RestartLevel;
            InitializeLeaderBoardPanel();
            InitializeMultiplayPanel();
            _UIController.SetScoreText(_scoreCounter.GameplayScore);
            _gameLoopEvents.OnWin += () => Win(interactableController);
            _gameLoopEvents.OnFail += () => Fail(interactableController);
        }

        private void InitializeLeaderBoardPanel()
        {
            _UIController.LeaderBoardPanel.OnRestart += RestartLevel;
            _UIController.LeaderBoardPanel.OnNextLevel += () =>
            {
                _progressData.Level.CurrentLevel += 1;
                _progressData.SaveProgress();
                RestartLevel();
            };
        }

        private void InitializeMultiplayPanel()
        {
            _UIController.MultiplayPanel.Onx1Multiplay += () => Multiplay(1);
            _UIController.MultiplayPanel.Onx3Multiplay += () => Multiplay(3);
            _UIController.MultiplayPanel.Onx5Multiplay += () => Multiplay(5);
        }

        private void Multiplay(int value)
        {
            _scoreCounter.Multiplay(value);
            _progressData.Score.CurrentScore = _scoreCounter.GameplayScore;
            _UIController.OpenLeaderboard(_progressData.Score.CurrentScore);
        }

        private void ScoreIncrease()
        {
            _scoreCounter.IncreaseScore();
            _UIController.SetScoreText(_scoreCounter.GameplayScore);
        }

        private void Fail(InteractableController interactableController)
        {
            interactableController.CanControl = false;
            _UIController.ActivateLosePanel();
        }

        private void Win(InteractableController interactableController)
        {
            interactableController.CanControl = false;
            _UIController.ActivateWinPanel();
        }
    }
}