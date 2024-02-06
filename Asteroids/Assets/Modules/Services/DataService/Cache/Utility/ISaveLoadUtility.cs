namespace Modules.Services.DataService
{
    public interface ISaveLoadUtility
    {
        void Save<T>(string path, T data) where T : IPersistentModel;
        T Load<T>(string path) where T : IPersistentModel;
    }
}