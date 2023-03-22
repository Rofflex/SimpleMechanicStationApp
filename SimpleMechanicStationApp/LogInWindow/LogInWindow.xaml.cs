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
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCollapsed_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            short Access;
            var dbFunctions = new dbFunctions();
            string Login = TextBoxLogin.Text.ToString();
            string Pass = PasswordBoxForPassword.Password;
            Access = dbFunctions.CheckLogPass(Login, Pass);
            this.Hide();
            switch (Access) {
                case 1:
                    Window generalForm = GeneralForm.MainFormClass.createForm();

                    generalForm.ShowDialog();
                    break;
                case 2:
                    MessageBox.Show("Check your login and password");
                    break;   
            }
            this.Show();
        }
        
    }
}
