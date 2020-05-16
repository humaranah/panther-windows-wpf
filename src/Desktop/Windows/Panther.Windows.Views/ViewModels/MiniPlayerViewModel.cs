using GalaSoft.MvvmLight;
using Panther.Core.Player;

namespace Panther.Windows.Views.ViewModels
{
    public class MiniPlayerViewModel : ViewModelBase
    {
        private IPlayerService _player;
        private IQueueService _queue;

        public MiniPlayerViewModel(IPlayerService player, IQueueService queue) : base()
        {
            _player = player;
            _queue = queue;
        }

        public void LoadFile(string fileName)
        {
            _player.Load(fileName);
        }
    }
}
