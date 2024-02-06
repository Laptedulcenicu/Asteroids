using Modules.Common;
using Modules.Services.Common;

namespace Modules.Services.DataService
{
    public class DataService : IDataService
    {
        public IPersistent Persistant { get; }
        public IApplicationCache ApplicationCache { get; }
        public IProgressData ProgressData { get; }

        public DataService()
        {
            ApplicationCache = new ApplicationCache();
            Persistant = new Persistent(ApplicationCache);
            ProgressData = new ProgressData(Persistant);
        }
    }
}