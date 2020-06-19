using NAudio.Wave;
using Panther.Core.Player.Events;
using System;

namespace Panther.Core.Player
{
    public sealed class CurrentAudioFileReaderAccessor
    {
        public event EventHandler<FileChangedEventArgs> FileChanged;

        private AudioFileReader _audioFileReader;

        public string FilePath { get; private set; }
        public bool IsDisposed { get; private set; } = true;
        public AudioFileReader Instance => IsDisposed ? null : _audioFileReader;

        public void Load(string filePath)
        {
            var previousFilePath = FilePath;
            if (!IsDisposed)
            {
                DisposeInternal();
            }

            FilePath = filePath;
            _audioFileReader = new AudioFileReader(filePath);
            IsDisposed = false;

            FileChanged?.Invoke(this, new FileChangedEventArgs(filePath, previousFilePath));
        }

        public void Dispose()
        {
            var previousFilePath = FilePath;
            DisposeInternal();
            FileChanged?.Invoke(this, new FileChangedEventArgs(null, previousFilePath));
        }

        private void DisposeInternal()
        {
            _audioFileReader?.Dispose();
            FilePath = null;
            IsDisposed = true;
        }
    }
}
