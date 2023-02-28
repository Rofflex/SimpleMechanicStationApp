using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SimpleMechanicStationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
            switch (Access) {
                case 1:
                    WpfLibrary1.Class1 class1 = new WpfLibrary1.Class1();
                    class1.test();
                    break;
                case 2:
                    MessageBox.Show("Check your login and password");
                    break;   
            }
        }
        
    }
}
