using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SimpleMechanicStationApp.OrderWindow.View;
using SimpleMechanicStationApp.GeneralVMM.OrderVMM.ViewModel;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CurrentUser _currentUser;//Current User Data
        private IDBCommands _dbCommands;//Commands to work with database

        public string CurrentUserName
        {
            get 
            {
                return _currentUser.Name;
            }
            set 
            {
                _currentUser.Name = value;
                OnPropertyChanged(nameof(CurrentUserName));
            }
        }

        public MainWindowViewModel(CurrentUser currentUser) 
        {
            _dbCommands = new DBCommands();
            _currentUser = currentUser;
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
            _currentUser = _dbCommands.DownloadUserAccount(_currentUser.Username);
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
