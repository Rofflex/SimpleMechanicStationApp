using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel
{
    public class OrderButtonViewModel:ViewModelBase
    {
        private OrderButton _orderButton;

        public bool IsEnabled
        {
            get => _orderButton.IsEnabled;
            set
            {
                _orderButton.IsEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public string Summary
        {
            get => _orderButton.Summary;
            set
            {
                _orderButton.Summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }

        public int OrderId
        {
            get => _orderButton.OrderId;
            set
            {
                _orderButton.OrderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }

        public OrderButtonViewModel()
        {
            _orderButton = new OrderButton();
        }
    }
}
