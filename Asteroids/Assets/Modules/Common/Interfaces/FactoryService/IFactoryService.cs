namespace Modules.Common
{
    public interface IFactoryService
    {
        public ISceneFactory SceneFactory { get; }
        public IServiceFactory ServiceFactory { get; }
        public IUIFactory UIFactory { get; }
    }
}