using Panther.Windows.Views.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Panther.Windows.Views
{
    /// <summary>
    /// Interaction logic for MiniPlayerWindow.xaml
    /// </summary>
    public partial class MiniPlayerWindow : Window
    {
        private readonly DispatcherTimer _timer;

        private IPlayerViewModel ViewModel => DataContext as IPlayerViewModel;

        public MiniPlayerWindow(IPlayerViewModel viewModel, DispatcherTimer timer)
        {
            InitializeComponent();
            DataContext = viewModel;
            _timer = timer;
            _timer.Tick += (s, e) => ViewModel.RaisePropertyChanged(nameof(ViewModel.Position));
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }

            var file = (e.Data.GetData(DataFormats.FileDrop) as string[])
                    .First();
            ViewModel.LoadFile(file);
            _timer.Start();
        }
    }
}
