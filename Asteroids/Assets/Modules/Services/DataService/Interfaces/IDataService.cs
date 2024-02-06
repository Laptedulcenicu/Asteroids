using Modules.Common;
using Modules.Services.Common;

namespace Modules.Services.DataService
{
    public interface IDataService
    {
        IPersistent Persistant { get; }
        IApplicationCache ApplicationCache { get; }
        IProgressData ProgressData { get; }
    }
}