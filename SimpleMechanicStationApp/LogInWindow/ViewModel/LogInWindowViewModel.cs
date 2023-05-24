﻿using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
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
        private CurrentUser _currentUser;
        private IDBCommands _dbCommands;

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
            _currentUser= new CurrentUser();
            _dbCommands = new DBCommands();
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
                    /*Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);*/
                    var newWindow = new MainWindow.View.MainWindow(_currentUser);
                    newWindow.Show();
                    CloseWindow();
                    break;
            }   
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
