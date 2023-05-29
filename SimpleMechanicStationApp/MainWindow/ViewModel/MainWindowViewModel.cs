using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SimpleMechanicStationApp.OrderWindow.View;
using SimpleMechanicStationApp.GeneralVMM.OrderVMM.ViewModel;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using System;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CurrentUser _currentUser;
        private IDBCommands _dbCommands;

        public string CurrentUserName
        {
            get => _currentUser.Name;
            set
            {
                _currentUser.Name = value;
                OnPropertyChanged(nameof(CurrentUserName));
            }
        }

        public ObservableCollection<OrderViewModel> Orders { get; }
        public ICommand OpenOrderCommand { get; }

        public MainWindowViewModel(CurrentUser currentUser)
        {
            _dbCommands = new DBCommands();
            _currentUser = currentUser;
            UpdateCurrentUser();

            Orders = new ObservableCollection<OrderViewModel>();
            var orders = _dbCommands.DownloadOrders();
            foreach (var order in orders)
            {
                Orders.Add(new OrderViewModel(order));
            }

            OpenOrderCommand = new ViewModelCommand<OrderViewModel>(ExecuteOpenOrderCommand);
        }

        private void UpdateCurrentUser()
        {
            _currentUser = _dbCommands.DownloadUserAccount(_currentUser.Username);
        }

        private void ExecuteOpenOrderCommand(OrderViewModel selectedOrder)
        {
            var orderWindowView = new OrderWindowView(_currentUser, selectedOrder.Order);
            orderWindowView.DataContext = selectedOrder;
            orderWindowView.Closed += OrderWindowViewClosed;
            orderWindowView.Show();
            selectedOrder.IsEnabled = false;
        }

        private void OrderWindowViewClosed(object? sender, EventArgs e)
        {
            if (sender is OrderWindowView orderWindowView && orderWindowView.DataContext is OrderViewModel orderViewModel)
            {
                orderViewModel.IsEnabled = true;
            }
        }
    }
}
