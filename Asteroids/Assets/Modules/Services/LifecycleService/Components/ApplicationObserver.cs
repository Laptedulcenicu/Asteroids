using System;
using Modules.Common;
using UnityEngine;

namespace Modules.Services.LifecycleService
{
    public class ApplicationObserver : MonoBehaviour, IApplicationObserver
    {
        public event Action ApplicationQuit;
        public event Action<bool> ApplicationPause;
        public event Action<bool> ApplicationFocus;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit() => ApplicationQuit?.Invoke();

        private void OnApplicationPause(bool pauseStatus) => ApplicationPause?.Invoke(pauseStatus);

       // private void OnApplicationFocus(bool hasFocus) => ApplicationFocus?.Invoke(hasFocus);
    }
}