using Modules.Common;
using UnityEngine;

namespace Modules.Services.FactoryService
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProviderService _assetProvider;

        public UIFactory(IAssetProviderService assetProviderService)
        {
            _assetProvider = assetProviderService;
        }

        public GameObject CreateUI() => _assetProvider.Instantiate(AssetPath.UI);
    }
}