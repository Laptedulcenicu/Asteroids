using Modules.Common;
using UnityEngine;

namespace Modules.Services.FactoryService
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IAssetProviderService _assetProvider;

        public ServiceFactory(IAssetProviderService assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateFadeController() => _assetProvider.Instantiate(AssetPath.FadeControllerPath);

        public GameObject CreateApplicationObserver() => _assetProvider.Instantiate(AssetPath.ApplicationObserverPath);

        public GameObject CreateMusicController() => _assetProvider.Instantiate(AssetPath.MusicControllerPath);

        public GameObject CreateSoundController() => _assetProvider.Instantiate(AssetPath.SoundControllerPath);
    }
}