using SimpleMechanicStationApp.GeneralVMM.CustomerM.Model;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;

namespace SimpleMechanicStationApp.CustomerWindow.ViewModel
{
    public class CustomerWindowViewModel:WindowVM<Customer, int>
    {
        // Const
        private const string selectQueryId = "select CustomerId from Customer where CustomerId = @CustomerId; ";
        private const string updateQuery = "update Customer " +
            "set CustomerId = @CustomerId, CustomerName = @CustomerName, CustomerPhone = @CustomerPhone, CustomerAddress = @CustomerAddress, CustomerInformation = @CustomerInformation " +
            "where CustomerId = @CustomerId;";
        private const string uploadQuery = "insert into Customer(CustomerId, CustomerName, CustomerPhone, CustomerAddress, CustomerInformation) " +
            "values(@CustomerId, @CustomerName, @CustomerPhone, @CustomerAddress, @CustomerInformation)";
        private const string getQuery = "select CustomerId, CustomerName, CustomerPhone, CustomerAddress, CustomerInformation " +
            "from Customer " +
            "where CustomerId = @Id";
        private const string getQueryId = "select top(1) (CustomerId+1) As CustomerId  " +
            "from Customer " +
            "order by CustomerId desc";

        // Fields

        // Properties
        public Customer Customer
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
        public int CustomerId
        {
            get => Item.CustomerId;
            set
            {
                Item.CustomerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }
        public string CustomerName
        {
            get => Item.CustomerName;
            set
            {
                Item.CustomerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }
        public string CustomerPhone
        {
            get => Item.CustomerPhone;
            set
            {
                Item.CustomerPhone = value;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }
        public string CustomerAddress
        {
            get => Item.CustomerAddress;
            set
            {
                Item.CustomerAddress = value;
                OnPropertyChanged(nameof(CustomerAddress));
            }
        }
        public string CustomerInformation
        {
            get => Item.CustomerInformation;
            set
            {
                Item.CustomerInformation = value;
                OnPropertyChanged(nameof(CustomerInformation));
            }
        }

        // Constructor
        /// <summary>
        /// Invoke constructor with getting information about customer
        /// </summary>
        /// <param name="customerId">is chosen customer</param>
        public CustomerWindowViewModel(int customerId): base(customerId, selectQueryId, updateQuery, uploadQuery, getQuery)
        {

        }
        /// <summary>
        /// Invoke constructor without any parameters to create new customer
        /// </summary>
        public CustomerWindowViewModel():base(selectQueryId, updateQuery, uploadQuery, getQueryId)
        {

        }

        // Methods

    }
}
