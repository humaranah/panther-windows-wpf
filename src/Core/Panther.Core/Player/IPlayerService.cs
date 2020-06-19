using System;

namespace Panther.Core.Player
{
    public interface IPlayerService : IDisposable
    {
        event PlaybackStoppedEventHandler PlaybackStopped;

        string FileName { get; }
        bool IsPlaying { get; set; }
        TimeSpan TrackLength { get; }
        TimeSpan TrackPosition { get; set; }
        float Volume { get; set; }

        void Load(string fileName);
        void Pause();
        void Play();
        void Stop();
    }
}
