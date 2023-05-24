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
            GeneralMethods.CreateForm.Buttons.ButtonActions buttonActions = new(this);
            Exit.Click += buttonActions.Exit_Click;
            Collapse.Click += buttonActions.Collapse_Click;
            MouseDown += buttonActions.Window_MouseDown;
        }
    }
}
