using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using SimpleMechanicStationApp.OrderWindow.View;
using SimpleMechanicStationApp.OrderWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Fields
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
        /// <summary>
        /// Invoke constructor with getting full information about current user using dbCommands.DownloadUserAccount()
        /// Then load Orders and assigning commands for OpenOrderButton and AddItem
        /// </summary>
        public MainWindowViewModel()
        {
            _dbCommands.DownloadUserAccount(_currentUser);
            Orders = new ObservableCollection<OrderButtonViewModel>();
            LoadOrders();
            OpenOrderButton = new ViewModelCommand<OrderButtonViewModel>(ExecuteOpenOrderButton);
            AddItem = new ViewModelCommand<object>(ExecuteAddItem);
        }

        // Methods
        /// <summary>
        /// Download Orders then comapare with existing orders using LINQ
        /// </summary>
        private void LoadOrders()
        {
            var orders = DownloadOrders();

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
        /// <summary>
        /// Gets brief information about the order and opening Order window with full information
        /// Also override OrderWindowViewClosed to connect OrderButtonViewModel and OrderViewModel. 
        /// In future want to make base class of brief order and then inherit it to full order
        /// </summary>
        /// <param name="selectedOrder">pressed button is selected order</param>
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
        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Gets list of brief information about list of orders
        /// </summary>
        /// <returns>List of OrderButtonViewModel</returns>
        public List<OrderButtonViewModel> DownloadOrders()
        {
            var orders = new List<OrderButtonViewModel>();
            using (var con = _dbCommands.GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select OrderId, (COALESCE(CarMake, '') +' '+ COALESCE(CarModel, '') +' '+ COALESCE(VIN, '') +' '" +
                            "+ COALESCE(Convert(varchar, CarYear), '') +' '+ COALESCE(CarPlate, '')) as \"Summary\" " +
                            "from dbo.[Order] inner join dbo.[CarInfo] on dbo.[Order].CarId = dbo.[CarInfo].CarId";
                        using (var reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                orders.Add(new OrderButtonViewModel
                                {
                                    OrderId = reader.GetInt32(0),
                                    Summary = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return orders;
        }
    }
}