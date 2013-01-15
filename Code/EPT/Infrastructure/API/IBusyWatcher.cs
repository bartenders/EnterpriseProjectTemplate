using EPT.Infrastructure.Framework;

namespace EPT.Infrastructure.API
{
    public interface IBusyWatcher
    {
        bool IsBusy { get; }

        BusyWatcher.BusyWatcherTicket GetTicket();

        void AddWatch();
        void RemoveWatch();
    }
}
