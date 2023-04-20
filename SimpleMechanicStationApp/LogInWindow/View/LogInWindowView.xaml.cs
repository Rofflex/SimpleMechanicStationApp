using System.Windows;
using System.Windows.Input;
using SimpleMechanicStationApp.LogInWindow.ViewModel;

namespace SimpleMechanicStationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInWindowView : Window
    {
        public LogInWindowView()
        {
            InitializeComponent();
            GeneralMethods.CreateForm.Buttons.ButtonActions buttonActions = new(this);
            ButtonExit.Click += buttonActions.Exit_Click;
            ButtonCollapse.Click += buttonActions.Collapse_Click;
            MouseDown += buttonActions.Window_MouseDown;
        }       
    }
}
