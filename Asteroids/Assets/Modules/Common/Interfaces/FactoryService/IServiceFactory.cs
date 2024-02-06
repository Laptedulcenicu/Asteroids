using UnityEngine;

namespace Modules.Common
{
    public interface IServiceFactory
    {
        GameObject CreateFadeController();
        GameObject CreateApplicationObserver();
        GameObject CreateMusicController();
        GameObject CreateSoundController();
    }
}