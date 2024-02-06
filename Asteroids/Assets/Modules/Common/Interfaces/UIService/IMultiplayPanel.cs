using System;

namespace Modules.Common
{
    public interface IMultiplayPanel
    {
        event Action Onx1Multiplay;
        event Action Onx3Multiplay;
        event Action Onx5Multiplay;
    }
}