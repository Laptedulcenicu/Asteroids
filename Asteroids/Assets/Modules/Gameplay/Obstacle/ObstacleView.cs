using System;
using Modules.Common;
using Modules.Gameplay;
using TMPro;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private AudioClip destroyAudio;
    [SerializeField] private int obstacleValue;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private InteractionDetection interactionDetection;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject collider;

    private int _currentObstacleValue;
    private IAudioService _audioService;
    private Action _onScoreIncrease;
    private Action _onDestroyObstacle;
    public bool IsDestroyed { get; private set; }

    public void Initialize(IAudioService audioService, Action scoreIncrease, Action destroyObstacle)
    {
        _audioService = audioService;
        _onScoreIncrease = scoreIncrease;
        _onDestroyObstacle = destroyObstacle;
        _currentObstacleValue = obstacleValue;
        valueText.text = obstacleValue.ToString();
        interactionDetection.OnInteract += Interact;
    }
    
    private void Interact(BulletView playerView)
    {
        _currentObstacleValue--;
        valueText.text = _currentObstacleValue.ToString();
        _onScoreIncrease?.Invoke();

        if (_currentObstacleValue <= 0)
        {
            Destroy();
        }
    }

    public void Move()
    {
    }

    private void Destroy()
    {
        IsDestroyed = true;
        collider.SetActive(false);
        model.SetActive(false);
        valueText.transform.parent.gameObject.SetActive(false);
        _onDestroyObstacle?.Invoke();
        _audioService.PlayOneShotSound(destroyAudio, 1f);
    }
}