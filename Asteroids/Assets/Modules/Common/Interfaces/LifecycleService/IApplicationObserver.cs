using System;

namespace Modules.Common
{
    public interface IApplicationObserver
    {
        event Action ApplicationQuit;
        event Action<bool> ApplicationPause;
        event Action<bool> ApplicationFocus;
    }
}