using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System;

namespace SimpleMechanicStationApp.GeneralVMM.OrderM.Model
{
    public class Order:ViewModelBase
    {
        private int _orderId;
        public int OrderId
        {
            get { return _orderId; }
            set
            {
                if (_orderId != value)
                {
                    _orderId = value;
                    OnPropertyChanged(nameof(OrderId));
                }
            }
        }

        private int _carId;
        public int CarId
        {
            get { return _carId; }
            set
            {
                if (_carId != value)
                {
                    _carId = value;
                    OnPropertyChanged(nameof(CarId));
                }
            }
        }

        private string _carMake;
        public string CarMake
        {
            get { return _carMake; }
            set
            {
                if (_carMake != value)
                {
                    _carMake = value;
                    OnPropertyChanged(nameof(CarMake));
                }
            }
        }

        private string _carModel;
        public string CarModel
        {
            get { return _carModel; }
            set
            {
                if (_carModel != value)
                {
                    _carModel = value;
                    OnPropertyChanged(nameof(CarModel));
                }
            }
        }

        private string _vin;
        public string VIN
        {
            get { return _vin; }
            set
            {
                if (_vin != value)
                {
                    _vin = value;
                    OnPropertyChanged(nameof(VIN));
                }
            }
        }

        private string _carPlate;
        public string CarPlate
        {
            get { return _carPlate; }
            set
            {
                if (_carPlate != value)
                {
                    _carPlate = value;
                    OnPropertyChanged(nameof(CarPlate));
                }
            }
        }

        private int _carYear;
        public int CarYear
        {
            get { return _carYear; }
            set
            {
                if (_carYear != value)
                {
                    _carYear = value;
                    OnPropertyChanged(nameof(CarYear));
                }
            }
        }

        private double _carOdometerStart;
        public double CarOdometerStart
        {
            get { return _carOdometerStart; }
            set
            {
                if (_carOdometerStart != value)
                {
                    _carOdometerStart = value;
                    OnPropertyChanged(nameof(CarOdometerStart));
                }
            }
        }

        private double _carOdometerFinish;
        public double CarOdometerFinish
        {
            get { return _carOdometerFinish; }
            set
            {
                if (_carOdometerFinish != value)
                {
                    _carOdometerFinish = value;
                    OnPropertyChanged(nameof(CarOdometerFinish));
                }
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        private string _customerPhone;
        public string CustomerPhone
        {
            get { return _customerPhone; }
            set
            {
                if (_customerPhone != value)
                {
                    _customerPhone = value;
                    OnPropertyChanged(nameof(CustomerPhone));
                }
            }
        }

        private string _customerAddress;
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set
            {
                if (_customerAddress != value)
                {
                    _customerAddress = value;
                    OnPropertyChanged(nameof(CustomerAddress));
                }
            }
        }

        private DateTime _orderOpenDate;
        public DateTime OrderOpenDate
        {
            get { return _orderOpenDate; }
            set
            {
                if (_orderOpenDate != value)
                {
                    _orderOpenDate = value;
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
            }
        }

        private DateTime _orderCloseDate;
        public DateTime OrderCloseDate
        {
            get { return _orderCloseDate; }
            set
            {
                if (_orderCloseDate != value)
                {
                    _orderCloseDate = value;
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
            }
        }

        private string _carName;
        public string CarName
        {
            get { return _carName; }
            set
            {
                if (_carName != value)
                {
                    _carName = value;
                    OnPropertyChanged(nameof(CarName));
                }
            }
        }

        private decimal _partsAmount;
        public decimal PartsAmount 
        {
            get { return _partsAmount; }
            set
            {
                if (_partsAmount != value)
                {
                    _partsAmount = value;
                    OnPropertyChanged(nameof(PartsAmount));
                    OnPropertyChanged(nameof(OrderAmount));
                }
            }
        }

        private decimal _salesTax;
        public decimal SalesTax
        {
            get { return _salesTax; }
            set
            {
                if (_salesTax != value)
                {
                    _salesTax = value;
                    OnPropertyChanged(nameof(SalesTax));
                }
            }
        }

        private decimal _wasteMaterialFee;
        public decimal WasteMaterialFee
        {
            get { return _wasteMaterialFee; }
            set
            {
                if (_wasteMaterialFee != value)
                {
                    _wasteMaterialFee = value;
                    OnPropertyChanged(nameof(WasteMaterialFee));
                }
            }
        }

        private decimal _laborsAmount;
        public decimal LaborsAmount
        {
            get { return _laborsAmount; }
            set
            {
                if (_laborsAmount != value)
                {
                    _laborsAmount = value;
                    OnPropertyChanged(nameof(LaborsAmount));
                    OnPropertyChanged(nameof(OrderAmount));
                }
            }
        }

        private decimal _subletRepair;
        public decimal SubletRepair
        {
            get { return _subletRepair; }
            set
            {
                if (_subletRepair != value)
                {
                    _subletRepair = value;
                    OnPropertyChanged(nameof(SubletRepair));
                }
            }
        }

        public decimal OrderAmount => Math.Round( ((LaborsAmount + PartsAmount) * (Discount==0?1:Discount/100)) - Deposit );

        private decimal _deposit;
        public decimal Deposit
        {
            get { return _deposit; }
            set
            {
                if (_deposit != value)
                {
                    _deposit = value;
                    OnPropertyChanged(nameof(Deposit));
                    OnPropertyChanged(nameof(OrderAmount));
                }
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged(nameof(Discount));
                    OnPropertyChanged(nameof(OrderAmount));
                }
            }
        }
    }
}
