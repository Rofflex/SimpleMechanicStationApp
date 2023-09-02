using SimpleMechanicStationApp.GeneralVMM.ManufactureM.Model;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;

namespace SimpleMechanicStationApp.ManufactureWindow.ViewModel
{
    public class ManufactureWindowViewModel:WindowVM<Manufacture, int>
    {
        // Constant
        private const string selectQueryId = "select ManufactureId from Manufacture where ManufactureId = @ManufactureId; ";
        private const string updateQuery = "update Manufacture " +
            "set ManufactureId = @ManufactureId, ManufactureName = @ManufactureName, ManufacturePhone = @ManufacturePhone " +
            "where ManufactureId = @ManufactureId;";
        private const string uploadQuery = "insert into Manufacture(ManufactureId, ManufactureName, ManufacturePhone) " +
            "values(@ManufactureId, @ManufactureName, @ManufacturePhone)";
        private const string getQuery = "select ManufactureId, ManufactureName, ManufacturePhone " +
            "from Manufacture " +
            "where ManufactureId = @Id";
        private const string getQueryId = "select top(1) (ManufactureId+1) As ManufactureId  " +
            "from Manufacture " +
            "order by ManufactureId desc";               

        // Fields

        // Properties
        public Manufacture Manufacture
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Manufacture));
            }
        }
        public int ManufactureId
        {
            get => Item.ManufactureId;
            set
            {
                Item.ManufactureId = value;
                OnPropertyChanged(nameof(ManufactureId));
            }
        }
        public string ManufactureName
        {
            get => Item.ManufactureName;
            set
            {
                Item.ManufactureName = value;
                OnPropertyChanged(nameof(ManufactureName));
            }
        }
        public string ManufacturePhone
        {
            get => Item.ManufacturePhone;
            set
            {
                Item.ManufacturePhone = value;
                OnPropertyChanged(nameof(ManufacturePhone));
            }
        }

        // Constructor
        public ManufactureWindowViewModel(int manufactureId):base(manufactureId, selectQueryId, updateQuery, uploadQuery, getQuery)
        {

        }
        public ManufactureWindowViewModel():base(selectQueryId, updateQuery, uploadQuery, getQueryId)
        {

        }

        // Methods

    }
}
