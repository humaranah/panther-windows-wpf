using System;

namespace Panther.Core.Library.Notification
{
    public interface IPeriodicObserver<T> : IObserver<T>
    {
        int Period { get; }
    }
}
