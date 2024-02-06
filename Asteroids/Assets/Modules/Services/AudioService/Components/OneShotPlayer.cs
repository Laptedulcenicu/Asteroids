using System;
using UnityEngine;

namespace Modules.Services.AudioService
{
    [RequireComponent(typeof(AudioSource))]
    public class OneShotPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Action<AudioSource> _callback;

        public void Play(AudioClip clip, float volume, Action<AudioSource> callback = null)
        {
            _callback = callback;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.PlayOneShot(clip, volume);
        }

        private void Update()
        {
            if (_audioSource.isPlaying)
            {
                return;
            }
            
            Destroy(this); //TODO Think more
            _callback?.Invoke(_audioSource);
        }
    }
}