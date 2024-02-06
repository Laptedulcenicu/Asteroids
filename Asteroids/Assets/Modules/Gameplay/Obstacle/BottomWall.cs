using System;
using Modules.Gameplay;
using UnityEngine;

public class BottomWall : MonoBehaviour
{
    [SerializeField] private InteractionDetection interactionDetection;

    private PlayerShoot _playerShoot;
    private Action _onMoveObstacles;
    private int _maxBallNumber;
    private int _currentTouchedBallNumber;
    private bool _isFirstTouch;

    public void Initialize(PlayerShoot playerShoot, Action onMoveObstacles, int maxBallNumber)
    {
        _playerShoot = playerShoot;
        _onMoveObstacles = onMoveObstacles;
        _maxBallNumber = maxBallNumber;
        _currentTouchedBallNumber = _maxBallNumber;
        interactionDetection.OnInteract += Interact;
    }

    private void Interact(BulletView bullet)
    {
        if (!bullet.IsMoving) return;
        bullet.Release();
        SetFirstTouchPosition(bullet);
        ChangeTouchedBallNumber();
    }

    private void SetFirstTouchPosition(BulletView bullet)
    {
        if (!_isFirstTouch)
        {
            _isFirstTouch = true;
            _playerShoot.PlayerPosition = new Vector3(bullet.transform.position.x, bullet.transform.position.y,
                bullet.transform.position.z + 0.1f);
        }
    }

    private void ChangeTouchedBallNumber()
    {
        _currentTouchedBallNumber--;
        if (_currentTouchedBallNumber > 0) return;
        _isFirstTouch = false;
        _currentTouchedBallNumber = _maxBallNumber;
        _onMoveObstacles?.Invoke();
    }
}