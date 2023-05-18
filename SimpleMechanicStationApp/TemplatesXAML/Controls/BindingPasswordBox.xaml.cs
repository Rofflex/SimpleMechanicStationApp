using System.Windows;

namespace SimpleMechanicStationApp.TemplatesXAML.Controls
{
    /// <summary>
    /// Logic for BindingPasswordBox.xaml
    /// It is necessary because of troubles with password box. It can't work without creating logic for dedicated passwordbox
    /// </summary>
    public partial class BindingPasswordBox //: UserControl
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
