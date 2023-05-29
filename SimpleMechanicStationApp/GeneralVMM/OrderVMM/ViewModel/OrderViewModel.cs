using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.OrderVMM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.OrderVMM.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        private bool _isEnabled = true;
        public OrderViewModel(Order order)
        {
            Order = order;
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            string Summary = $"{Order.CarName} {Order.CarVIN} {Order.CarYear.Year} {Order.CarPlate}";
            Order.Summary = Summary;
        }

        public Order Order { get; set; }

        public string Summary => Order.Summary;

        public bool IsEnabled 
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
    }
}
