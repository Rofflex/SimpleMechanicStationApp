using System.Windows;
using System.Windows.Input;
using SimpleMechanicStationApp.LogInWindow.ViewModel;

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
            DataContext= new LogInWindowViewModel();
        }       
    }
}
