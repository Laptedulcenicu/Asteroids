using System;
using Modules.Common;
using UnityEngine;

namespace Modules.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerShoot playerShoot;
        [SerializeField] private GameObject reflectionLine;

        public PlayerShoot PlayerShoot => playerShoot;

        public GameObject ReflectionLine => reflectionLine;

        public void Initialize(GameLoopEvents gameLoopEvents, IAudioService audioService, int maxBallCount)
        {
            playerShoot.Initialize(gameLoopEvents,audioService,maxBallCount);
        }
        
    }
}