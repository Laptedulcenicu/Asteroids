using Modules.Common;

namespace Modules.Services.FactoryService
{
    public class FactoryService : IFactoryService
    {
        public ISceneFactory SceneFactory { get; }
        public IServiceFactory ServiceFactory { get; }
        public IUIFactory UIFactory { get; }

        public FactoryService(IAssetProviderService assetProviderService)
        {
            SceneFactory = new SceneFactory(assetProviderService);
            ServiceFactory = new ServiceFactory(assetProviderService);
            UIFactory = new UIFactory(assetProviderService);
        }
    }
}