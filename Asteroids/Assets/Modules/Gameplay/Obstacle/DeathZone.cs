using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Action _onFail;
    private bool _isFailed;

    public void Initialize(Action failGame) => _onFail = failGame;

    private void OnTriggerEnter(Collider other) => GameFail();

    private void GameFail()
    {
        if (_isFailed) return;
        _isFailed = true;
        _onFail?.Invoke();
    }
}