using System;
using System.Collections.Generic;

namespace Panther.Core.Library.Notification
{
    public sealed class LoadStateUnsubscriber : IDisposable
    {
        private readonly List<IObserver<ProgressNotification>> _observers;
        private readonly IObserver<ProgressNotification> _current;

        public LoadStateUnsubscriber(
            List<IObserver<ProgressNotification>> observers, IObserver<ProgressNotification> current)
        {
            _observers = observers;
            _current = current;
        }

        public void Dispose()
        {
            if (_observers != null && _observers.Contains(_current))
            {
                _observers.Remove(_current);
            }
        }
    }
}
