using NAudio.Wave;
using System;

namespace Panther.Core.Player
{
    public sealed class PlayerService : IPlayerService
    {
        private readonly CurrentAudioFileReaderAccessor _audioAccessor;
        private readonly WaveOutEvent _outputDevice;


        public event PlaybackStoppedEventHandler PlaybackStopped;


        public PlayerService(CurrentAudioFileReaderAccessor audioAccessor)
        {
            _outputDevice = new WaveOutEvent();
            _audioAccessor = audioAccessor;

            _outputDevice.PlaybackStopped += HandleInternalPlaybackStopped;
        }


        #region Properties
        public string FileName { get; private set; }

        public float Volume
        {
            get => _outputDevice.Volume;
            set => _outputDevice.Volume = value;
        }

        public bool IsPlaying
        {
            get => _outputDevice.PlaybackState == PlaybackState.Playing;
            set
            {
                if (_audioAccessor.IsDisposed)
                {
                    return;
                }

                if (value && _outputDevice.PlaybackState != PlaybackState.Playing)
                {
                    _outputDevice.Play();
                    return;
                }

                if (!value && _outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    _outputDevice.Pause();
                }
            }
        }

        public TimeSpan TrackPosition
        {
            get => _audioAccessor.Instance?.CurrentTime ?? TimeSpan.Zero;
            set
            {
                if (!_audioAccessor.IsDisposed)
                {
                    _audioAccessor.Instance.CurrentTime = value;
                }
            }
        }

        public TimeSpan TrackLength => _audioAccessor.Instance?.TotalTime ?? TimeSpan.Zero;
        #endregion


        #region Methods
        public void Dispose()
        {
            _audioAccessor?.Dispose();
            _outputDevice?.Dispose();
            FileName = null;
        }

        public void Load(string fileName)
        {
            FileName = FileName;
            _outputDevice.Stop();
            _audioAccessor.Load(fileName);
            _outputDevice.Init(_audioAccessor.Instance);
        }

        public void Pause() => _outputDevice.Pause();

        public void Play() => _outputDevice.Play();

        public void Stop() => _outputDevice.Stop();

        private void HandleInternalPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _audioAccessor.Instance.CurrentTime = TimeSpan.Zero;
            PlaybackStopped?.Invoke(sender, new PlaybackStoppedEventArgs(
                _audioAccessor.Instance.Length,
                _audioAccessor.Instance.Position));
        }
        #endregion
    }
}
