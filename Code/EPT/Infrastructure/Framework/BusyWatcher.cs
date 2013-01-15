using Caliburn.Micro;
using EPT.Infrastructure.API;
using System;
using System.ComponentModel.Composition;
using System.Threading;

namespace EPT.Infrastructure.Framework
{
    [Export(typeof(IBusyWatcher))]
    public class BusyWatcher : PropertyChangedBase, IBusyWatcher
    {
        private int _counter;

        public bool IsBusy
        {
            get
            {
                return _counter > 0;
            }
        }

        public BusyWatcherTicket GetTicket()
        {
            return new BusyWatcherTicket(this);
        }

        public void AddWatch()
        {
            if (Interlocked.Increment(ref _counter) == 1)
            {
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public void RemoveWatch()
        {
            if (Interlocked.Decrement(ref _counter) == 0)
            {
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public class BusyWatcherTicket : IDisposable
        {
            private readonly IBusyWatcher _parent;

            public BusyWatcherTicket(IBusyWatcher parent)
            {
                _parent = parent;
                _parent.AddWatch();
            }

            public void Dispose()
            {
                _parent.RemoveWatch();
            }
        }
    }
}
