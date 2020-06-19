using Panther.Core.Player.Events;
using System;
using TagLib;

namespace Panther.Core.Player
{
    public sealed class CurrentAudioInformationReader : IAudioInformationReader<File>, IDisposable
    {
        private readonly CurrentAudioFileReaderAccessor _accessor;

        public CurrentAudioInformationReader(CurrentAudioFileReaderAccessor accessor)
        {
            _accessor = accessor;
            _accessor.FileChanged += UpdateInformation;
        }

        public File Information { get; private set; }

        public void Dispose()
        {
            Information?.Dispose();
        }

        private void UpdateInformation(object sender, FileChangedEventArgs e)
        {
            if (e.CurrentPath == null)
            {
                Dispose();
                return;
            }

            Information = File.Create(_accessor.FilePath);
        }
    }
}
