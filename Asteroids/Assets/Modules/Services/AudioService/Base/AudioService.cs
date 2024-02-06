using System;
using Modules.Common;
using UnityEngine;

namespace Modules.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly MusicController _musicController;
        private readonly SoundController _soundController;

        public AudioService(IServiceFactory serviceFactory)
        {
            _musicController = serviceFactory.CreateMusicController().GetComponent<MusicController>();
            _soundController = serviceFactory.CreateSoundController().GetComponent<SoundController>();
        }

        public void PlayMusic()
        {
            _musicController.PlayMusic();
        }

        public void PlayOneShotSound(AudioClip clip, float volume, Action callback = null)
        {
            _soundController.PlayOneShotAndRelease(clip, volume, callback);
        }
    }
}