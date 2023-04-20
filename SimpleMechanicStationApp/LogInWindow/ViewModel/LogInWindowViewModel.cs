using SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Release;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBase;
using SimpleMechanicStationApp.GeneralMethods.WindowMethods.OpenCloseCommands;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.LogInWindow.ViewModel
{
    public class LogInWindowViewModel : ViewModelBase
    {

        private string _username;
        private string _password;
        private bool _isViewVisible = true;

        private IDbCommands _dbCommands;
        private IOpenCloseCommands _openCloseCommands;

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }

        public LogInWindowViewModel()
        {
            _dbCommands = new DbWorking();
            _openCloseCommands = new OpenCloseCommands();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand(""));
        }

        private void ExecuteRecoverPassCommand(string v1)
        {
            throw new NotImplementedException();
        }

        /*private bool CanExecuteLoginCommand(object obj)
        {
            bool flag;
            if (Username is null || Password is null)
            {
                flag = false;
            }
            else { flag = true; }
            return flag;
        }*/

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = _dbCommands.AuthUser(Username, Password);
            switch (isValidUser) 
            {
                case 0:
                    MessageBox.Show("Database connection not established");
                    break;

                case 1:
                    MessageBox.Show("Wrong Login or Password");
                    break;
                case 2:
                    var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    var newWindow = new MainWindow.MainWindow();
                    _openCloseCommands.OpenWindow(newWindow);
                    _openCloseCommands.CloseWindow(currentWindow);
                    break;
            }   
        }
    }
}
