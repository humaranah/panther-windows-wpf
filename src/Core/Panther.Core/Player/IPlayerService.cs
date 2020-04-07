using System;

namespace Panther.Core.Player
{
    public interface IPlayerService : IDisposable
    {
        TimeSpan TrackPosition { get; set; }
        float Volume { get; set; }

        void Load(string fileName);
        void Pause();
        void Play();
        void Stop();
    }
}
