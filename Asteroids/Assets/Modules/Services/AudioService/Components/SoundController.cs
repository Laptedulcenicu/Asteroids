using System;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace Modules.Services.AudioService
{
    public class SoundController : MonoBehaviour
    {
        private ObjectPool<AudioSource> _sourcePool;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _sourcePool = new ObjectPool<AudioSource>(
                CreateAudioSource,
                GetAudioSource,
                ReleaseAudioSource
            );
        }

        private AudioSource CreateAudioSource()
        {
            var go = new GameObject(nameof(AudioSource));
            go.transform.SetParent(transform);
            var audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            go.SetActive(true);
            return audioSource;
        }

        private static void GetAudioSource(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(true);
        }

        private static void ReleaseAudioSource(AudioSource audioSource)
        {
            audioSource.clip = null;
            audioSource.volume = 1;
            audioSource.loop = false;
            audioSource.gameObject.SetActive(false);
        }

        private AudioSource Source => _sourcePool.Get();

        private void Release(AudioSource source) => _sourcePool.Release(source);

        public void PlayOneShotAndRelease(AudioClip clip, float volume, Action callback = null)
        {
            var player = Source.gameObject.AddComponent<OneShotPlayer>();
            player.Play(clip, volume, source =>
            {
                Release(source);
                callback?.Invoke();
            });
        }
    }
}