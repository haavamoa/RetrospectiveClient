using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using RetrospectiveClient.ViewModel;

namespace RetrospectiveClient.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ShellViewModel shellViewModel)
            {
                await Task.Run(() => shellViewModel.Initialize());
            }
        }
    }
}