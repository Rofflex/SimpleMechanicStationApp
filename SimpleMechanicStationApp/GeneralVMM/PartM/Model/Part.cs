using System;

namespace SimpleMechanicStationApp.GeneralVMM.PartM.Model
{
    public class Part : BaseModel<string>
    {
        // Fields
        private string _partDescription;
        private string _partLink;
        private int _manufactureId;
        private string _manufactureName;
        private decimal _partRetailPrice;
        private decimal _partTradePrice;
        private decimal _quantity;
        private decimal _partSoldPrice;
        
        // Properties 
        public string PartId 
        {
            get => Id;
            set 
            {
                Id = value;
                OnPropertyChanged(nameof(PartId));
            }
        }
        public string PartName 
        {
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged(nameof(PartName));
            }
        }
        public string PartDescription 
        {
            get => _partDescription;
            set 
            {
                _partDescription = value;
                OnPropertyChanged(nameof(PartDescription));
            }
        }
        public string PartLink
        {
            get => _partLink;
            set
            {
                _partLink = value;
                OnPropertyChanged(nameof(PartLink));
            }
        }
        public int ManufactureId
        {
            get => _manufactureId;
            set
            {
                _manufactureId = value;
                OnPropertyChanged(nameof(ManufactureId));
            }
        }
        public string ManufactureName 
        {
            get => _manufactureName;
            set 
            {
                _manufactureName= value;
                OnPropertyChanged(nameof(ManufactureName));
            }
        }
        public decimal PartRetailPrice 
        {
            get => _partRetailPrice;
            set 
            {
                _partRetailPrice= value;
                OnPropertyChanged(nameof(PartRetailPrice));
            }
        }
        public decimal PartTradePrice 
        {
            get => _partTradePrice;
            set 
            {
                _partTradePrice= value;
                OnPropertyChanged(nameof(PartTradePrice));
            }
        }
        public decimal Quantity 
        {
            get => _quantity;
            set 
            { 
                _quantity = value; 
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Summary));
            }
        }
        public decimal PartSoldPrice
        {
            get => _partSoldPrice;
            set
            {
                _partSoldPrice = value;
                OnPropertyChanged(nameof(PartSoldPrice));
                OnPropertyChanged(nameof(Summary));
            }
        }
        public decimal Summary => Math.Round(PartSoldPrice * Quantity, 2);
    }
}
