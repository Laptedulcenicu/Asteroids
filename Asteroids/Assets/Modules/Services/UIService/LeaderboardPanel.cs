using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Common;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LeaderboardPanel : MonoBehaviour, ILeaderBoardPanel
{
    private const string k_Player = "Player ";
    private const string k_You = "You";

    public event Action OnNextLevel = delegate { };
    public event Action OnRestart = delegate { };

    [SerializeField] private RectTransform content;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private PlayerData playerDataPrefab;
    [SerializeField] private ScrollRect scrollRect;
    private List<PlayerData> _playerDataList = new();
    private PlayerData _currentPlayer;
    private readonly Vector2 _randomScore = new(30, 9999);

    private void Awake()
    {
        restartButton.onClick.AddListener(() => OnRestart?.Invoke());
        nextLevelButton.onClick.AddListener(() => OnNextLevel?.Invoke());
    }

    public void SetUpLeaderboard(int score)
    {
        CreatePlayers(score);
        SetPlayersOrder();
        AutoScrollToPlayerData();
    }

    private void AutoScrollToPlayerData()
    {
        int index = _playerDataList.IndexOf(_currentPlayer);
        RectTransform targetElement = content.GetChild(index) as RectTransform;
        Vector2 targetElementPosition = new Vector2(0, -targetElement.anchoredPosition.y);
        float normalizedPosition = targetElementPosition.y / (content.sizeDelta.y - scrollRect.viewport.sizeDelta.y);
        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        float finalValue = 1f - normalizedPosition;

  
    }

    private void SetPlayersOrder()
    {
        _playerDataList = _playerDataList.OrderByDescending(e => e.Score).ToList();
        int order = 1;
        for (var i = 0; i < _playerDataList.Count; i++)
        {
            _playerDataList[i].SetOrder(order);
            _playerDataList[i].transform.SetParent(content);
            order++;
        }
    }

    private void CreatePlayers(int score)
    {
        PlayerData playerData = Instantiate(playerDataPrefab);
        playerData.Initialize(k_You, score);
        playerData.SetColor(Color.red);
        _currentPlayer = playerData;
        _playerDataList.Add(playerData);

        for (int i = 0; i < 99; i++)
        {
            var fakePlayer = Instantiate(playerDataPrefab);
            var name = k_Player + i + 1;
            var fakePlayerScore = (int)Random.Range(_randomScore.x, _randomScore.y);
            fakePlayer.Initialize(name, fakePlayerScore);
            _playerDataList.Add(fakePlayer);
        }
    }
}