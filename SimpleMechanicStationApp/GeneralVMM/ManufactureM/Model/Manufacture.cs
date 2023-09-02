
namespace SimpleMechanicStationApp.GeneralVMM.ManufactureM.Model
{
    public class Manufacture:BaseModel<int>
    {
        // Fields
        private string _manufacturePhone;

        // Properties
        public int ManufactureId 
        {
            get => Id;
            set 
            {
                Id = value;
                OnPropertyChanged(nameof(ManufactureId));
            }
        }
        public string ManufactureName 
        {
            get => Name;
            set 
            {
                Name = value;
                OnPropertyChanged(nameof(ManufactureName));
            }
        }
        public string ManufacturePhone 
        {
            get => _manufacturePhone;
            set 
            {
                _manufacturePhone = value;
                OnPropertyChanged(nameof(ManufacturePhone));
            }
        }
    }
}
