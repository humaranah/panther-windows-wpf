using System;

namespace Panther.Core.Player
{
    public delegate void PlaybackStoppedEventHandler(object sender, PlaybackStoppedEventArgs e);

    public class PlaybackStoppedEventArgs : EventArgs
    {
        public PlaybackStoppedEventArgs(long length, long position) : base()
        {
            TrackPosition = position;
            TrackLength = length;
        }

        public long TrackPosition { get; }
        public long TrackLength { get; }

        public bool IsPlaybackComplete => TrackPosition == TrackLength;
    }
}
