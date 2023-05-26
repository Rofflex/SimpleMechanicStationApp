using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.MainWindow.ViewModel;
using System.Windows;


namespace SimpleMechanicStationApp.MainWindow.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(CurrentUser currentUser)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(currentUser);
        }
    }
}
