using System.Collections.Generic;
using Modules.Common;

namespace Modules.Services.LifecycleService
{
    public class LifecycleService : ILifecycleService
    {
        private readonly List<ILifecycleDelegate> _subscribers = new();

        public LifecycleService(IServiceFactory factoryService)
        {
            var applicationObserver = factoryService.CreateApplicationObserver().GetComponent<ApplicationObserver>();
            AddListeners(applicationObserver);
        }

        private void AddListeners(IApplicationObserver applicationObserver)
        {
            applicationObserver.ApplicationQuit += InvokeApplicationQuit;
            applicationObserver.ApplicationPause += InvokeApplicationPause;
            applicationObserver.ApplicationFocus += InvokeApplicationFocus;
        }

        private void InvokeApplicationQuit()
        {
            foreach (var subscriber in _subscribers)
                subscriber.OnApplicationQuit();
        }

        private void InvokeApplicationPause(bool pauseStatus)
        {
            foreach (var subscriber in _subscribers)
                subscriber.OnApplicationPause(pauseStatus);
        }

        private void InvokeApplicationFocus(bool hasFocus)
        {
            foreach (var subscriber in _subscribers)
                subscriber.OnApplicationFocus(hasFocus);
        }

        public void AddDelegate(ILifecycleDelegate del)
        {
            _subscribers.Add(del);
        }

        public void RemoveDelegate(ILifecycleDelegate del)
        {
            for (var i = _subscribers.Count - 1; i >= 0; i--)
            {
                if (_subscribers[i] != del)
                    continue;

                _subscribers.RemoveAt(i);
                break;
            }
        }
    }
}