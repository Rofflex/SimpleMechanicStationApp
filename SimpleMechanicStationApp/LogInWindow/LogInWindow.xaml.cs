using System.Windows;
using System.Windows.Input;
using SimpleMechanicStationApp.LogInForm.Methods;


namespace SimpleMechanicStationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
            GeneralMethods.CreateForm.Buttons.ButtonActions buttonActions = new(this);
            ButtonExit.Click += buttonActions.Exit_Click;
            ButtonCollapse.Click += buttonActions.Collapse_Click;
            MouseDown += buttonActions.Window_MouseDown;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            short Access;
            var dbFunctions = new dbFunctions();
            string Login = TextBoxLogin.Text.ToString();
            string Pass = PasswordBoxForPassword.Password;
            Access = dbFunctions.CheckLogPass(Login, Pass);
            switch (Access) {
                case 1:
                    MainWindow.MainWindow mainWindow = new MainWindow.MainWindow();
                    mainWindow.Show();
                    this.Close();
                    break;
                case 2:
                    MessageBox.Show("Check your login and password");
                    break;   
            }
        }
        
    }
}
