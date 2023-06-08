using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using SimpleMechanicStationApp.OrderWindow.View;
using SimpleMechanicStationApp.OrderWindow.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDBCommands _dbCommands = DBCommands.Instance;
        private readonly CurrentUser _currentUser = CurrentUser.Instance;

        public string CurrentUserName
        {
            get => _currentUser.Name;
            set
            {
                _currentUser.Name = value;
                OnPropertyChanged(nameof(CurrentUserName));
            }
        }

        public ObservableCollection<OrderButtonViewModel> Orders { get; }
        public ICommand OpenOrderButton { get; }

        public MainWindowViewModel()
        {
            _dbCommands.DownloadUserAccount(_currentUser);
            Orders = new ObservableCollection<OrderButtonViewModel>();
            LoadOrders();

            OpenOrderButton = new ViewModelCommand<OrderButtonViewModel>(ExecuteOpenOrderButtonCommand);
        }


        private void LoadOrders()
        {
            var orders = _dbCommands.DownloadOrders();
            foreach (var order in orders)
            {
                var orderButtonViewModel = new OrderButtonViewModel()
                {
                    OrderId = order.OrderId,
                    Summary = order.Summary,
                    IsEnabled = true
                };
                Orders.Add(orderButtonViewModel);
            }
        }

        private void ExecuteOpenOrderButtonCommand(OrderButtonViewModel selectedOrder)
        {
            var orderWindowViewModel = new OrderWindowViewModel(selectedOrder.OrderId);
            var orderWindowView = new OrderWindowView()
            {
                DataContext = orderWindowViewModel
            };
            orderWindowView.Closed += OrderWindowViewClosed;
            orderWindowView.Show();

            selectedOrder.IsEnabled = false;
        }

        private void OrderWindowViewClosed(object? sender, EventArgs e)
        {
            if (sender is OrderWindowView orderWindowView && orderWindowView.DataContext is OrderWindowViewModel orderWindowViewModel)
            {
                foreach (var order in Orders)
                {
                    if (orderWindowViewModel.OrderId == order.OrderId)
                    {
                        order.IsEnabled = true;
                        break;
                    }
                }
            }
        }
    }
}