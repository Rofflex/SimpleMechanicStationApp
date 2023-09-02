using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.LogInWindow.ViewModel
{
    public class LogInWindowViewModel : ViewModelBase
    {
        // Class fields
        private readonly IDBCommands _dbCommands = DBCommands.Instance;
        private readonly CurrentUser _currentUser = CurrentUser.Instance;

        // Properties
        public string Username
        {
            get => _currentUser.Username;
            set
            {
                _currentUser.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _currentUser.Password;
            set
            {
                _currentUser.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand Login { get; }
        public ICommand RecoverPassword { get; }

        // Constructor
        public LogInWindowViewModel()
        {
            Login = new ViewModelCommand<object>(ExecuteLogin);
            RecoverPassword = new ViewModelCommand<object>(ExecuteRecoverPassword);
        }

        // Methods
        private void ExecuteRecoverPassword(object obj)
        {
            // TODO: Implement password recovery logic
            throw new NotImplementedException();
        }

        private void ExecuteLogin(object obj)
        {
            var isValidUser = _dbCommands.AuthUser(Username, Password);

            switch (isValidUser)
            {
                case 0:
                    MessageBox.Show("Database connection is not established");
                    break;

                case 1:
                    MessageBox.Show("Wrong Login or Password");
                    break;

                case 2:
                    var currentWindow = Application.Current.Windows.OfType<Window>()
                        .FirstOrDefault(window => window.DataContext == this);
                    if (currentWindow != null)
                    {
                        var newWindow = new MainWindow.View.MainWindow();
                        newWindow.Show();
                        currentWindow.Close();
                    }
                    break;
            }
        }
    }

}
