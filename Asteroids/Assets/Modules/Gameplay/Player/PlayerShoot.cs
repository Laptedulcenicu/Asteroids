using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Common;
using StansAssets.Foundation.Patterns;
using TMPro;
using UnityEngine;

namespace Modules.Gameplay
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private AudioClip shootAudio;
        [SerializeField] private Transform bulletParent;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private TextMeshProUGUI ballCounterText;

        private int _maxBullets;
        private int _countBalls;
        private PrefabPool _bulletPool;
        private BulletView _currentActiveBullet;
        private IAudioService _audioService;
        private readonly WaitForSeconds _waitForSeconds = new(0.1f);
        private readonly List<BulletView> _currentBulletList = new();
        private Coroutine _shootCoroutine;
        public Vector3 PlayerPosition { get; set; }

        public void Initialize(GameLoopEvents gameLoopEvents, IAudioService audioService, int maxBallCount)
        {
            _audioService = audioService;
            _bulletPool = new PrefabPool(bulletPrefab);
            _maxBullets = maxBallCount;
            _countBalls = _maxBullets;
            gameLoopEvents.OnWin += DisableActiveBalls;
            InitializeBullet();
        }

        public void Shoot()
        {
            _shootCoroutine=  StartCoroutine(ShootBall());
        }

        public void Reload()
        {
            _countBalls = _maxBullets;
            transform.transform.position = PlayerPosition;
            ballCounterText.enabled = true;
            InitializeBullet();
        }

        private void SetTextValue()
        {
            ballCounterText.text = "x" + _countBalls;
        }

        private void InitializeBullet()
        {
            _currentActiveBullet = GetBullet();
            _currentActiveBullet.Initialize(_bulletPool);
            _currentActiveBullet.transform.position = bulletParent.position;
            SetTextValue();
        }

        private IEnumerator ShootBall()
        {
            while (_countBalls >= 0)
            {
                yield return _waitForSeconds;
                var bulletTransform = _currentActiveBullet.transform;
                bulletTransform.eulerAngles = bulletParent.eulerAngles;
                _currentActiveBullet.Move(bulletTransform.forward);
                _audioService.PlayOneShotSound(shootAudio, 1);

                DecreaseBallCount();
            }
        }

        private void DecreaseBallCount()
        {
            _countBalls--;
        
            if (_countBalls > 0)
            {
                InitializeBullet();
            }
            else
            {
                ballCounterText.enabled = false;
            }
        }

        private BulletView GetBullet()
        {
            BulletView bulletView = _bulletPool.Get().GetComponent<BulletView>();
            _currentBulletList.Add(bulletView);
            return bulletView;
        }

        private void DisableActiveBalls()
        {
            StopCoroutine(_shootCoroutine);
            foreach (var bulletView in _currentBulletList)
            {
                if (bulletView.IsMoving)
                {
                    bulletView.Release();
                }
            }
        }
    }
}