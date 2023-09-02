using System.Windows;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using SimpleMechanicStationApp.GeneralVMM.MechanicM.Model;

namespace SimpleMechanicStationApp.MechanicWindow.ViewModel
{
    public class MechanicWindowViewModel:WindowVM<Mechanic, int>
    {
        // Constant
        private const string selectQueryId = "select MechanicId from Mechanic where MechanicId = @MechanicId; ";
        private const string updateQuery = "update Mechanic " +
            "set MechanicId = @MechanicId, MechanicName = @MechanicName " +
            "where MechanicId = @MechanicId;";
        private const string uploadQuery = "insert into Mechanic(MechanicId, MechanicName) " +
            "values(@MechanicId, @MechanicName)";
        private const string getQuery = "select MechanicId, MechanicName " +
            "from Mechanic " +
            "where MechanicId = @Id";
        private const string getQueryId = "select top(1) (MechanicId+1) As MechanicId  " +
            "from Mechanic " +
            "order by MechanicId desc";

        // Fields

        // Properties
        public Mechanic Mechanic
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Mechanic));
            }
        }
        public int MechanicId
        {
            get => Item.MechanicId;
            set
            {
                Item.MechanicId = value;
                OnPropertyChanged(nameof(MechanicId));
            }
        }
        public string MechanicName
        {
            get => Item.MechanicName;
            set
            {
                Item.MechanicName = value;
                OnPropertyChanged(nameof(MechanicName));
            }
        }

        // Constructor
        public MechanicWindowViewModel(int mechanicId):base(mechanicId, selectQueryId,updateQuery, uploadQuery, getQuery)
        {

        }
        public MechanicWindowViewModel():base(selectQueryId, updateQuery, uploadQuery, getQueryId)
        {

        }

        // Methods

    }
}
