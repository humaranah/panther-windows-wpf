using MvvmCross.ViewModels;
using Panther.Core.Player;
using System;
using System.Linq;
using System.Threading.Tasks;
using TagLib;

namespace Panther.Windows.Views.ViewModels
{
    public interface IPlayerViewModel : IMvxNotifyPropertyChanged
    {
        string SongInfo { get; }
        IPicture AlbumArt { get; }
        bool IsPlaying { get; set; }
        long Length { get; }
        long Position { get; set; }
        long Volume { get; set; }

        void LoadFile(string fileName);
    }

    public sealed class PlayerViewModel : MvxViewModel, IPlayerViewModel
    {
        private readonly IQueueService _queue;
        private readonly IPlayerService _player;
        private readonly IAudioInformationReader<File> _infoReader;

        public PlayerViewModel(IQueueService queue, IPlayerService player, IAudioInformationReader<File> infoReader)
            : base()
        {
            _queue = queue;
            _player = player;
            _infoReader = infoReader;

            _player.PlaybackStopped += (s,e) => RaiseAllPropertiesChanged();
        }

        private File AudioInfo => _infoReader.Information;

        public string SongInfo => $"{AudioInfo?.Tag.FirstPerformer} - {AudioInfo?.Tag.Title}";

        public IPicture AlbumArt => AudioInfo?.Tag.Pictures.FirstOrDefault();

        public bool IsPlaying
        {
            get => _player.IsPlaying;
            set
            {
                _player.IsPlaying = value;
                RaisePropertyChanged();
            }
        }

        public long Length => (long)_player.TrackLength.TotalSeconds;

        public long Position
        {
            get => (long)_player.TrackPosition.TotalSeconds;
            set
            {
                _player.TrackPosition = TimeSpan.FromSeconds(value);
                RaisePropertyChanged();
            }
        }

        public long Volume
        {
            get => (long)(_player.Volume * 100);
            set
            {
                _player.Volume = (float)value / 100;
                RaisePropertyChanged();
            }
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public void LoadFile(string fileName)
        {
            _player.Load(fileName);
            _player.Play();

            RaiseAllPropertiesChanged();
        }
    }
}
