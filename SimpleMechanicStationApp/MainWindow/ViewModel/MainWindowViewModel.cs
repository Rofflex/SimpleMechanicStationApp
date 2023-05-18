using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBase;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using System.Threading;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Release;
using System.Collections.ObjectModel;
using SimpleMechanicStationApp.GeneralVMM.ViewModel;
using SimpleMechanicStationApp.GeneralVMM.Model;
using System.Windows.Input;
using SimpleMechanicStationApp.OrderWindow.View;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DbCurrentUserModel _currentUser;//Current User Data
        private IDbCommands _dbCommands;//Commands to work with database

        public MainWindowViewModel() 
        {
            _dbCommands = new DbWorking();
            _currentUser = new DbCurrentUserModel();
            updateCurrentUser();

            Orders = new ObservableCollection<OrderViewModel>();
            var orders = _dbCommands.DownloadOrders();
            foreach (var order in orders)
            {
                Orders.Add(new OrderViewModel(order));
            }

            OpenOrderCommand = new ViewModelCommand(ExecuteOpenOrderCommand);
        }

        public ObservableCollection<OrderViewModel> Orders { get; set; }
        public ICommand OpenOrderCommand { get; }

        private void updateCurrentUser()
        {
            _currentUser = _dbCommands.DownloadUserAccount(Thread.CurrentPrincipal.Identity.Name);
        }
        private void ExecuteOpenOrderCommand(object obj)
        {
            var clickedOrderViewModel = (OrderViewModel)obj;
            var orderWindowView = new OrderWindowView(_currentUser, clickedOrderViewModel.Order);
            orderWindowView.DataContext = _currentUser;
            orderWindowView.Show();
        }
    }
}
