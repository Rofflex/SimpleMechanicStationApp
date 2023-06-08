using SimpleMechanicStationApp.LogInWindow.ViewModel;
using System.Windows;

namespace SimpleMechanicStationApp.LogInWindow.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInWindowView : Window
    {
        public LogInWindowView()
        {
            InitializeComponent();
            DataContext = new LogInWindowViewModel();
        }
    }
}
