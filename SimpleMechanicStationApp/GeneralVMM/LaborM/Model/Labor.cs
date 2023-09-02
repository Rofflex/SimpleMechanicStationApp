using System;

namespace SimpleMechanicStationApp.GeneralVMM.LaborM.Model
{
    public class Labor:BaseModel<int>
    {
        // Fields
        private string _laborDescription;
        private string _laborLink;
        private decimal _laborHours;
        private int _mechanicId;
        private string _mechanicName;
        private decimal _laborSoldPrice;
        private decimal _laborSoldHours;
        private string _partId;

        // Properties
        public int LaborId 
        {
            get => Id;
            set 
            {
                Id = value;
                OnPropertyChanged(nameof(LaborId));
            }
        }
        public string LaborName 
        {
            get => Name;
            set 
            {
                Name = value;
                OnPropertyChanged(nameof(LaborName));
            }
        }
        public string LaborDescription 
        {
            get => _laborDescription;
            set 
            {
                _laborDescription = value;
                OnPropertyChanged(nameof(LaborDescription));
            }
        }
        public string LaborLink
        {
            get => _laborLink;
            set
            {
                _laborLink = value;
                OnPropertyChanged(nameof(LaborLink));
            }
        }
        public decimal LaborHours
        {
            get => _laborHours;
            set
            {
                _laborHours = value;
                OnPropertyChanged(nameof(LaborHours));
            }
        }
        public int MechanicId 
        {
            get => _mechanicId;
            set 
            {
                _mechanicId = value;
                OnPropertyChanged(nameof(MechanicId));
            }
        }
        public string MechanicName 
        {
            get => _mechanicName;
            set 
            {
                _mechanicName = value;
                OnPropertyChanged(nameof(MechanicName));
            }
        }
        public decimal LaborSoldPrice
        {
            get => Math.Round(LaborSoldHours * 150, 2);
            set
            {
                _laborSoldPrice = value;
                OnPropertyChanged(nameof(LaborSoldPrice));
            }
        }
        public decimal LaborSoldHours 
        {
            get => _laborSoldHours;
            set
            {
                _laborSoldHours = value;
                OnPropertyChanged(nameof(LaborSoldHours));
                OnPropertyChanged(nameof(LaborSoldPrice));
            }
        }
        public string PartId 
        {
            get => _partId;
            set 
            {
                _partId = value;
                OnPropertyChanged(nameof(PartId));
            }
        }
    }
}
