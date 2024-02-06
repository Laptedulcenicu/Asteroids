namespace Modules.Common
{
    public interface ILifecycleService
    {
        void AddDelegate(ILifecycleDelegate del);

        void RemoveDelegate(ILifecycleDelegate del);
    }
}