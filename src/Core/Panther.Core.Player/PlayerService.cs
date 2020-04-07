using NAudio.Wave;
using System;

namespace Panther.Core.Player
{
    public sealed class PlayerService : IPlayerService
    {
        private readonly AudioFileReaderAccessor _audioAccessor;
        private readonly WaveOutEvent _outputDevice;

        public PlayerService(AudioFileReaderAccessor audioAccessor)
        {
            _outputDevice = new WaveOutEvent();
            _audioAccessor = audioAccessor;
        }

        public float Volume
        {
            get => _outputDevice.Volume;
            set => _outputDevice.Volume = value;
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

        public void Dispose()
        {
            _audioAccessor?.Dispose();
            _outputDevice?.Dispose();
        }

        public void Load(string fileName)
        {
            _outputDevice.Stop();
            _audioAccessor.Load(fileName);
            _outputDevice.Init(_audioAccessor.Instance);
        }

        public void Pause() => _outputDevice.Pause();

        public void Play() => _outputDevice.Play();

        public void Stop() => _outputDevice.Stop();
    }
}
