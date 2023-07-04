using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralVMM.LaborM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderM.Model;
using SimpleMechanicStationApp.GeneralVMM.PartM.Model;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMechanicStationApp.OrderWindow.ViewModel
{
    public class OrderWindowViewModel : ViewModelBase
    {
        // Constant
        private const string QueryForParts = "select PartOrder.PartId, CarPart.PartDescription, " +
            "Manufacture.ManufactureName, CarPart.PartRetailPrice, CarPart.PartTradePrice, " +
            "PartOrder.Quantity, PartOrder.PartSoldPrice " +
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

        // Class fields
        private bool _isReadOnly;
        private bool _isEditing;
        private readonly IDBCommands _dbCommands = DBCommands.Instance;
        private ObservableCollection<Part> _parts;
        private ObservableCollection<Labor> _labors;
        private Order _order;
        
        // Properties
        public bool IsReadOnly 
        {
            get { return _isReadOnly; }
            set 
            { 
                _isReadOnly = value; 
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public ObservableCollection<Part> Parts
        {
            get { return _parts; }
            set
            {
                _parts = value;
                OnPropertyChanged(nameof(Parts));
            }
        }

        public ObservableCollection<Labor> Labors
        {
            get { return _labors; }
            set
            {
                _labors = value;
                OnPropertyChanged(nameof(Labors));
            }
        }

        public int OrderId
        {
            get { return _order.OrderId; }
            set
            {
                _order.OrderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }

        public string CarMake
        {
            get { return _order.CarMake; }
            set
            {
                _order.CarMake = value;
                OnPropertyChanged(nameof(CarMake));
            }
        }

        public string CarModel
        {
            get { return _order.CarModel; }
            set
            {
                _order.CarModel = value;
                OnPropertyChanged(nameof(CarModel));
            }
        }

        public string VIN
        {
            get { return _order.VIN; }
            set
            {
                _order.VIN = value;
                OnPropertyChanged(nameof(VIN));
            }
        }

        public string CarPlate
        {
            get { return _order.CarPlate; }
            set
            {
                _order.CarPlate = value;
                OnPropertyChanged(nameof(CarPlate));
            }
        }

        public int CarYear
        {
            get { return _order.CarYear; }
            set
            {
                _order.CarYear = value;
                OnPropertyChanged(nameof(CarYear));
            }
        }

        public double CarOdometerStart
        {
            get { return _order.CarOdometerStart; }
            set
            {
                _order.CarOdometerStart = value;
                OnPropertyChanged(nameof(CarOdometerStart));
            }
        }

        public double CarOdometerFinish
        {
            get { return _order.CarOdometerFinish; }
            set
            {
                _order.CarOdometerFinish = value;
                OnPropertyChanged(nameof(CarOdometerFinish));
            }
        }

        public string CarName
        {
            get { return _order.CarName; }
            set
            {
                _order.CarName = value;
                OnPropertyChanged(nameof(CarName));
            }
        }

        public string CustomerName
        {
            get { return _order.CustomerName; }
            set
            {
                _order.CustomerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        public string CustomerAddress
        {
            get { return _order.CustomerAddress; }
            set
            {
                _order.CustomerAddress = value;
                OnPropertyChanged(nameof(CustomerAddress));
            }
        }

        public string CustomerPhone
        {
            get { return _order.CustomerPhone; }
            set
            {
                _order.CustomerPhone = value;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }

        public string OrderOpenDate
        {
            get
            {
                if (_order.OrderOpenDate != DateTime.MinValue)
                {
                    return _order.OrderOpenDate.ToString("MM.dd.yyyy");
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
                    _order.OrderOpenDate = DateTime.MinValue;
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
                else
                {
                    _order.OrderOpenDate = Convert.ToDateTime(value);
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
            }
        }

        public string OrderCloseDate
        {
            get
            {
                if (_order.OrderCloseDate != DateTime.MinValue)
                {
                    return _order.OrderCloseDate.ToString("MM.dd.yyyy");
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
                    _order.OrderCloseDate = DateTime.MinValue;
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
                else
                {
                    _order.OrderCloseDate = Convert.ToDateTime(value);
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
            }
        }

        public decimal PartsAmount
        {
            get => _order.PartsAmount;
            set
            {
                _order.PartsAmount = value;
                OnPropertyChanged(nameof(PartsAmount));
            }
        }

        public decimal LaborsAmount
        {
            get { return _order.LaborsAmount; }
            set
            {
                _order.LaborsAmount = value;
                OnPropertyChanged(nameof(LaborsAmount));
            }
        }

        public decimal SubletRepair
        {
            get { return _order.SubletRepair; }
            set
            {
                _order.SubletRepair = value;
                OnPropertyChanged(nameof(SubletRepair));
            }
        }

        public decimal Discount
        {
            get { return _order.Discount; }
            set
            {
                _order.Discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }

        public decimal OrderAmount
        {
            get { return _order.OrderAmount; }
        }

        public decimal Deposit
        {
            get { return _order.Deposit; }
            set
            {
                _order.Deposit = value;
                OnPropertyChanged(nameof(Deposit));
            }
        }

        public ICommand DefinateColumnsChanged { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        // Constructor
        public OrderWindowViewModel(int order)
        {
            _isReadOnly = true;
            _isEditing = false;
            _parts = new ObservableCollection<Part>(_dbCommands.GetItemsForOrder<Part>(order, QueryForParts));
            _labors = new ObservableCollection<Labor>(_dbCommands.GetItemsForOrder<Labor>(order, QueryForLabors));
            _order = GetOrder(order);
            DefinateColumnsChanged = new ViewModelCommand<object>(ExecuteDefinateColumnsChanged);
            EditCommand = new ViewModelCommand<object>(ExecuteEditCommand);
            SaveCommand = new ViewModelCommand<object>(ExecuteSaveCommand);
        }

        // Methods
        private void ExecuteDefinateColumnsChanged(object param)
        {
            switch ((string)param)
            {
                case "Sold price" or "Quantity":
                    PartsAmount = GetSummaryOfColumn<Part>(Parts, "Summary");
                    OnPropertyChanged(nameof(OrderAmount));
                    break;
                case "Hours":
                    LaborsAmount = GetSummaryOfColumn<Labor>(Labors, "LaborSoldPrice");
                    OnPropertyChanged(nameof(OrderAmount));
                    break;
            }
        }

        private decimal GetSummaryOfColumn<T>(ObservableCollection<T> Collection, string ColumnName)
        {
            decimal summary = 0;
            if (Collection.Count != 0)
            {
                Type type = Collection[0].GetType();
                PropertyInfo propertyInfo = type.GetProperty(ColumnName);
                foreach (var item in Collection)
                {
                    if (propertyInfo != null)
                    {
                        decimal columnValue = (decimal)propertyInfo.GetValue(item);
                        summary += columnValue;
                    }
                }
            }
            return summary;
        }

        private Order GetOrder(int orderId)
        {
            Order? order = _dbCommands.UpdateOrder(orderId);
            order.PartsAmount = GetSummaryOfColumn<Part>(Parts, "Summary");
            order.LaborsAmount = GetSummaryOfColumn<Labor>(Labors, "LaborSoldPrice");
            return order;
        }

        private void ExecuteEditCommand(object param) 
        {
            if (IsReadOnly)
            {
                IsEditing = true;
                IsReadOnly = false;
            }
        }

        private void ExecuteSaveCommand(object param)
        {
            if (IsEditing)
            {
                IsEditing = false;
                IsReadOnly = true;
            }
        }
    }

}

