using Modules.Common;
using UnityEngine;

namespace Modules.Services.FactoryService
{
    public class SceneFactory : ISceneFactory
    {
        private readonly IAssetProviderService _assetProvider;

        public SceneFactory(IAssetProviderService assetProviderService)
        {
            _assetProvider = assetProviderService;
        }

        public GameObject CreatePlayer() => _assetProvider.Instantiate(AssetPath.PlayerPath);

    }
}