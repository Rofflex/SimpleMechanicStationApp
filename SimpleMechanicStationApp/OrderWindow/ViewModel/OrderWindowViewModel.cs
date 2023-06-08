using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.LaborM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderM.Model;
using SimpleMechanicStationApp.GeneralVMM.PartM.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMechanicStationApp.OrderWindow.ViewModel
{
    public class OrderWindowViewModel : ViewModelBase
    {
        private const string QueryForParts = "select PartOrder.PartId, CarPart.PartDescription, " +
                            "Manufacture.ManufactureName, CarPart.PartRetailPrice, CarPart.PartTradePrice, " +
                            "PartOrder.Quantity, PartOrder.PartSoldPrice, [Order].PartsAmount " +
                            "from [Order] inner join PartOrder on [Order].OrderId=[PartOrder].OrderId " +
                            "inner join CarPart on PartOrder.PartId=CarPart.PartId " +
                            "inner join Manufacture on CarPart.ManufactureId=Manufacture.ManufactureId " +
                            "where [Order].OrderId=@OrderId";
        private const string QueryForLabors = "select LaborOrder.LaborId, LaborName, LaborHours, MechanicName, " +
                            "LaborSoldPrice, LaborsAmount " +
                            "from [Order] inner join LaborOrder on [Order].OrderId=LaborOrder.OrderId " +
                            "inner join Labor on LaborOrder.LaborId=Labor.LaborId " +
                            "inner join Mechanic on Mechanic.MechanicId=LaborOrder.MechanicId " +
                            "where [Order].OrderId=@OrderId";
        private IDBCommands _dbCommands;
        public OrderWindowViewModel(int order)
        {
            _dbCommands = DBCommands.Instance;
            Order = _dbCommands.UpdateOrder(order);
            Parts = new ObservableCollection<Part>(_dbCommands.GetItemsForOrder<Part>(order, QueryForParts));
            Labors = new ObservableCollection<Labor>(_dbCommands.GetItemsForOrder<Labor>(order, QueryForLabors));
        }

        public Order Order { get; set; }
        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Labor> Labors { get; set; }

        public int OrderId 
        {
            get { return Order.OrderId; }
            set 
            {
                Order.OrderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }

        public string CarMake
        {
            get { return Order.CarMake; }
            set
            {
                Order.CarMake = value;
                OnPropertyChanged(nameof(CarMake));
            }
        }

        public string CarModel
        {
            get { return Order.CarModel; }
            set
            {
                Order.CarModel = value;
                OnPropertyChanged(nameof(CarModel));
            }
        }

        public string VIN
        {
            get { return Order.VIN; }
            set
            {
                Order.VIN = value;
                OnPropertyChanged(nameof(VIN));
            }
        }
        public string CarPlate
        {
            get { return Order.CarPlate; }
            set
            {
                Order.CarPlate = value;
                OnPropertyChanged(nameof(CarPlate));
            }
        }

        public int CarYear
        {
            get { return Order.CarYear; }
            set
            {
                Order.CarYear = value;
                OnPropertyChanged(nameof(CarYear));
            }
        }

        public double CarOdometerStart
        {
            get { return Order.CarOdometerStart; }
            set
            {
                Order.CarOdometerStart = value;
                OnPropertyChanged(nameof(CarOdometerStart));
            }
        }

        public double CarOdometerFinish
        {
            get { return Order.CarOdometerFinish; }
            set
            {
                Order.CarOdometerFinish = value;
                OnPropertyChanged(nameof(CarOdometerFinish));
            }
        }

        public string CarName
        {
            get { return Order.CarName; }
            set
            {
                Order.CarName = value;
                OnPropertyChanged(nameof(CarName));
            }
        }
        public string CustomerName
        {
            get { return Order.CustomerName; }
            set
            {
                Order.CustomerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        public string CustomerAddress
        {
            get { return Order.CustomerAddress; }
            set
            {
                Order.CustomerAddress = value;
                OnPropertyChanged(nameof(CustomerAddress));
            }
        }

        public string CustomerPhone
        {
            get { return Order.CustomerPhone; }
            set
            {
                Order.CustomerPhone = value;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }

        public string OrderOpenDate
        {
            get 
            {
                if (Order.OrderOpenDate != DateTime.MinValue)
                {
                    return Order.OrderOpenDate.ToString("MM.dd.yyyy");
                }
                else 
                {
                    return null;
                }
            }
            set
            {
                if (value is null)
                {
                    Order.OrderOpenDate = DateTime.MinValue;
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
                else 
                {
                    Order.OrderOpenDate = Convert.ToDateTime(value);
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
            }
        }

        public string OrderCloseDate
        {
            get
            {
                if (Order.OrderCloseDate != DateTime.MinValue)
                {
                    return Order.OrderCloseDate.ToString("MM.dd.yyyy"); ;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value is null)
                {
                    Order.OrderCloseDate = DateTime.MinValue;
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
                else
                {
                    Order.OrderCloseDate = Convert.ToDateTime(value);
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
            }
        }

    }
}
   
