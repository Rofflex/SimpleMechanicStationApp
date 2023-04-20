using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace SimpleMechanicStationApp.TemplatesXAML.Controls
{
    /// <summary>
    /// Логика взаимодействия для BindingPasswordBox.xaml
    /// </summary>
    public partial class BindingPasswordBox : UserControl
    {
            public static readonly DependencyProperty PasswordProperty =
                DependencyProperty.Register("Password", typeof(string), typeof(BindingPasswordBox));

            public string Password
            {
                get { return (string)GetValue(PasswordProperty); }
                set { SetValue(PasswordProperty, value); }
            }

            public BindingPasswordBox()
            {
                InitializeComponent();
                PasswordBoxForPassword.PasswordChanged += OnPasswordChanged;

            }

            private void OnPasswordChanged(object sender, RoutedEventArgs e)
            {
                Password = (string)PasswordBoxForPassword.Password;
            }
    }
}
