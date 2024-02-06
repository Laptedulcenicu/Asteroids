namespace Modules.Services.DataService
{
    public interface ISerializer
    {
        object Serialize<T>(T data);
        T Deserialize<T>(object data);
    }
}