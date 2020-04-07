using NAudio.Wave;

namespace Panther.Core.Player
{
    public sealed class AudioFileReaderAccessor
    {
        private AudioFileReader _audioFileReader;

        public bool IsDisposed { get; private set; } = true;
        public AudioFileReader Instance
        {
            get => IsDisposed ? null : _audioFileReader;
        }

        public void Load(string audioFile)
        {
            if (!IsDisposed)
            {
                Dispose();
            }

            _audioFileReader = new AudioFileReader(audioFile);
            IsDisposed = false;
        }

        public void Dispose()
        {
            _audioFileReader.Dispose();
            IsDisposed = true;
        }
    }
}
