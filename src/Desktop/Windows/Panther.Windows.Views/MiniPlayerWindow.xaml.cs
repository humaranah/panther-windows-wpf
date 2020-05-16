using Panther.Windows.Views.ViewModels;
using System.Linq;
using System.Windows;

namespace Panther.Windows.Views
{
    /// <summary>
    /// Interaction logic for MiniPlayerWindow.xaml
    /// </summary>
    public partial class MiniPlayerWindow : Window
    {
        private MiniPlayerViewModel ViewModel => DataContext as MiniPlayerViewModel;

        public MiniPlayerWindow(MiniPlayerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var file = (e.Data.GetData(DataFormats.FileDrop) as string[])
                    .First();
            ViewModel.LoadFile(file);
        }
    }
}
