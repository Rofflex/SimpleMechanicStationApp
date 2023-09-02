using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;

namespace SimpleMechanicStationApp.GeneralVMM.CarM.Model
{
    public class Car:BaseModel<int>
    {
        // Fields
        private string _carMake;
        private string _carModel;
        private int _carYear;
        private string _carEngine;
        private string _carBodyStyle;
        private string _carTrimLevel;
        private string _carWheelDrive;
        private string _carTransmission;
        private string _color;
        private string _carAdditional;
        private string _carLink;
        private string _vin;

        // Properties
        public int CarId
        {
            get => Id;
            set
            {
                Id = value;
                OnPropertyChanged(nameof(CarId));
            }
        }
        public string CarMake
        {
            get => _carMake;
            set
            {
                _carMake = value;
                OnPropertyChanged(nameof(CarMake));
            }
        }
        public string CarModel
        {
            get => _carModel;
            set
            {
                _carModel = value;
                OnPropertyChanged(nameof(CarModel));
            }
        }
        public int CarYear
        {
            get => _carYear;
            set
            {
                _carYear = value;
                OnPropertyChanged(nameof(CarYear));
            }
        }
        public string CarEngine
        {
            get => _carEngine;
            set
            {
                _carEngine = value;
                OnPropertyChanged(nameof(CarEngine));
            }
        }
        public string CarBodyStyle
        {
            get => _carBodyStyle;
            set
            {
                _carBodyStyle = value;
                OnPropertyChanged(nameof(CarBodyStyle));
            }
        }
        public string CarTrimLevel
        {
            get => _carTrimLevel;
            set
            {
                _carTrimLevel = value;
                OnPropertyChanged(nameof(CarTrimLevel));
            }
        }
        public string CarWheelDrive
        {
            get => _carWheelDrive;
            set
            {
                _carWheelDrive = value;
                OnPropertyChanged(nameof(CarWheelDrive));
            }
        }
        public string CarTransmission
        {
            get => _carTransmission;
            set
            {
                _carTransmission = value;
                OnPropertyChanged(nameof(CarTransmission));
            }
        }
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        public string CarAdditional
        {
            get => _carAdditional;
            set
            {
                _carAdditional = value;
                OnPropertyChanged(nameof(CarAdditional));
            }
        }
        public string CarLink
        {
            get => _carLink;
            set
            {
                _carLink = value;
                OnPropertyChanged(nameof(CarLink));
            }
        }
        public string CarName
        {
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged(nameof(CarName));
            }
        }
        public string VIN
        {
            get => _vin;
            set
            {
                _vin = value;
                OnPropertyChanged(nameof(VIN));
            }
        }
    }
}
