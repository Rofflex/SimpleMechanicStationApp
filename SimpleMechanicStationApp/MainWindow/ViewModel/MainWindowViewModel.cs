using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.LaborM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using SimpleMechanicStationApp.OrderWindow.View;
using SimpleMechanicStationApp.OrderWindow.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Class fields
        private readonly IDBCommands _dbCommands = DBCommands.Instance;
        private readonly CurrentUser _currentUser = CurrentUser.Instance;

        // Properties
        public string CurrentUserName
        {
            get => _currentUser.Name;
            set
            {
                _currentUser.Name = value;
                OnPropertyChanged(nameof(CurrentUserName));
            }
        }
        public ObservableCollection<OrderButtonViewModel> Orders { get; set; }
        public ICommand OpenOrderButton { get; set; }
        public ICommand AddItem { get; set; }

        //Constructor
        public MainWindowViewModel()
        {
            _dbCommands.DownloadUserAccount(_currentUser);
            Orders = new ObservableCollection<OrderButtonViewModel>();
            LoadOrders();
            OpenOrderButton = new ViewModelCommand<OrderButtonViewModel>(ExecuteOpenOrderButton);
            AddItem = new ViewModelCommand<object>(ExecuteAddItem);
        }

        // Methods
        private void LoadOrders()
        {
            var orders = _dbCommands.DownloadOrders();

            var ordersToRemove = Orders.Where(order => !orders.Any(newOrder => newOrder.OrderId == order.OrderId)).ToList();
            foreach (var orderToRemove in ordersToRemove)
            {
                Orders.Remove(orderToRemove);
            }

            foreach (var order in orders)
            {
                var existingOrder = Orders.FirstOrDefault(existing => existing.OrderId == order.OrderId);
                if (existingOrder != null)
                {
                    existingOrder.Summary = order.Summary;
                }
                else
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
        }
        private void ExecuteOpenOrderButton(OrderButtonViewModel selectedOrder)
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
                LoadOrders();
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
        private void ExecuteAddItem(object obj) 
        {
            var orderWindowViewModel = new OrderWindowViewModel();
            var orderWindowView = new OrderWindowView()
            {
                DataContext = orderWindowViewModel
            };
            orderWindowView.Closed += OrderWindowViewClosed;
            orderWindowView.Show();
        }
    }
}