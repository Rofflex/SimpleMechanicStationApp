using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System;

namespace SimpleMechanicStationApp.GeneralVMM.PartM.Model
{
    public class Part : ViewModelBase
    {
        private string _partId;
        public string PartId
        {
            get { return _partId; }
            set
            {
                if (_partId != value)
                {
                    _partId = value;
                    OnPropertyChanged(nameof(PartId));
                }
            }
        }

        private string _partDescription;
        public string PartDescription
        {
            get { return _partDescription; }
            set
            {
                if (_partDescription != value)
                {
                    _partDescription = value;
                    OnPropertyChanged(nameof(PartDescription));
                }
            }
        }

        private string _manufactureName;
        public string ManufactureName
        {
            get { return _manufactureName; }
            set
            {
                if (_manufactureName != value)
                {
                    _manufactureName = value;
                    OnPropertyChanged(nameof(ManufactureName));
                }
            }
        }

        private decimal _partRetailPrice;
        public decimal PartRetailPrice
        {
            get { return _partRetailPrice; }
            set
            {
                if (_partRetailPrice != value)
                {
                    _partRetailPrice = value;
                    OnPropertyChanged(nameof(PartRetailPrice));
                }
            }
        }

        private decimal _partTradePrice;
        public decimal PartTradePrice
        {
            get { return _partTradePrice; }
            set
            {
                if (_partTradePrice != value)
                {
                    _partTradePrice = value;
                    OnPropertyChanged(nameof(PartTradePrice));
                }
            }
        }

        private decimal _quantity;
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(Summary));
                }
            }
        }

        private decimal _partSoldPrice;
        public decimal PartSoldPrice
        {
            get { return _partSoldPrice; }
            set
            {
                if (_partSoldPrice != value)
                {
                    _partSoldPrice = value;
                    OnPropertyChanged(nameof(PartSoldPrice));
                    OnPropertyChanged(nameof(Summary));
                }
            }
        }
        public decimal Summary => Math.Round(PartSoldPrice * Quantity);
    }
}
