using SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Release;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBase;
using SimpleMechanicStationApp.GeneralMethods.WindowMethods.OpenCloseCommands;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.LogInWindow.ViewModel
{
    public class LogInWindowViewModel : ViewModelBase
    {

        /*private string _username;
        private string _password;
        */
        private DbCurrentUserModel _currentUser;
        private IDbCommands _dbCommands;
        private IOpenCloseCommands _openCloseCommands;

        public string Username
        {
            get
            {
                return _currentUser.Username;
            }

            set
            {
                _currentUser.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get
            {
                return _currentUser.Password;
            }

            set
            {
                _currentUser.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }

        public LogInWindowViewModel()
        {
            _currentUser= new DbCurrentUserModel();
            _dbCommands = new DbWorking();
            _openCloseCommands = new OpenCloseCommands();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand(""));
        }

        private void ExecuteRecoverPassCommand(string v1)
        {
            throw new NotImplementedException();
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = _dbCommands.AuthUser(_currentUser);
            switch (isValidUser) 
            {
                case 0:
                    MessageBox.Show("Database connection not established");
                    break;

                case 1:
                    MessageBox.Show("Wrong Login or Password");
                    break;
                case 2:
                    Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                    var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    var newWindow = new MainWindow.MainWindow();
                    _openCloseCommands.OpenWindow(newWindow);
                    _openCloseCommands.CloseWindow(currentWindow);
                    break;
            }   
        }
    }
}
