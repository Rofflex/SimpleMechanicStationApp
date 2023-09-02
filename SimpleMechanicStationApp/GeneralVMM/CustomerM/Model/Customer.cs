
namespace SimpleMechanicStationApp.GeneralVMM.CustomerM.Model
{
    public class Customer: BaseModel<int>
    {
        // Fields
        private string _customerPhone;
        private string _customerAddress;
        private string _customerInformation;

        // Properties
        public int CustomerId
        {
            get => Id;
            set
            {
                Id = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }
        public string CustomerName
        {
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }
        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                _customerPhone = value;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }
        public string CustomerAddress
        {
            get => _customerAddress;
            set
            {
                _customerAddress = value;
                OnPropertyChanged(nameof(CustomerAddress));
            }
        }
        public string CustomerInformation
        {
            get => _customerInformation;
            set
            {
                _customerInformation = value;
                OnPropertyChanged(nameof(CustomerInformation));
            }
        }
    }
}
