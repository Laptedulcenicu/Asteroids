namespace Modules.Common
{
    public interface IProgressData : ILifecycleDelegate
    {
        Level Level { get; }
        Score Score { get; }

        void SaveProgress();

        void LoadProgress();
    }
}