using System;
using Modules.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Services.UIService
{
    public class UIController : MonoBehaviour, IUIController
    {
        public event Action OnPlay = delegate { };
        public event Action OnRestart = delegate { };

        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject HUDPanel;
        [SerializeField] private MultiplayPanel multiplayPanel;
        [SerializeField] private LeaderboardPanel leaderboardPanel;

        [SerializeField] private TextMeshProUGUI textLevel;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button reloadLevel;
        [SerializeField] private EventTrigger playButton;

        public IMultiplayPanel MultiplayPanel => multiplayPanel;
        public ILeaderBoardPanel LeaderBoardPanel => leaderboardPanel;

        private void Awake()
        {
            restartButton.onClick.AddListener(() => OnRestart?.Invoke());
            reloadLevel.onClick.AddListener(() => OnRestart?.Invoke());
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((_) => OnPlay?.Invoke());
            playButton.triggers.Add(entry);
        }
        
        public void Initialize(int level)
        {
            textLevel.text = level.ToString();
        }

        public void Play()
        {
            mainMenuPanel.SetActive(false);
            HUDPanel.SetActive(true);
        }

        public void ActivateLosePanel()
        {
            losePanel.SetActive(true);
            HUDPanel.SetActive(false);
        }

        public void ActivateWinPanel()
        {
            HUDPanel.SetActive(false);
            multiplayPanel.gameObject.SetActive(true);
            losePanel.SetActive(false);
        }

        public void SetScoreText(int score) => scoreText.text = score.ToString();

        public void OpenLeaderboard(int scoreCurrentScore)
        {
            leaderboardPanel.SetUpLeaderboard(scoreCurrentScore);
            multiplayPanel.gameObject.SetActive(false);
            leaderboardPanel.gameObject.SetActive(true);
        }
    }
}