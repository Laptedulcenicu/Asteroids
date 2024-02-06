using System;
using UnityEngine;

namespace Modules.Common
{
    public interface IAudioService
    {
        void PlayMusic();

        void PlayOneShotSound(AudioClip clip, float volume, Action callback = null);
    }
}