using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Windows.Input;
using SimpleMechanicStationApp.GeneralVMM.LaborM.Model;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using System.Collections.Generic;
using SimpleMechanicStationApp.GeneralVMM.PartM.Model;

namespace SimpleMechanicStationApp.LaborWindow.ViewModel
{
    public class LaborWindowViewModel:WindowVM<Labor,int>
    {
        // Constant
        private const string selectQueryId = "select LaborId from Labor where LaborId = @LaborId and PartId = @PartId; ";
        private const string selectQueryNew = "select LaborId from Labor where LaborId = @LaborId; ";
        private const string updateQuery = "update Labor " +
            "set LaborId = @LaborId, LaborName = @LaborName, LaborDescription = @LaborDescription, LaborLink = @LaborLink, LaborHours = @LaborHours " +
            "where LaborId = @LaborId and PartId = @PartId;";
        private const string updateQueryNew = "update Labor " +
            "set LaborId = @LaborId, LaborName = @LaborName, LaborDescription = @LaborDescription, LaborLink = @LaborLink, LaborHours = @LaborHours " +
            "where LaborId = @LaborId;";
        private const string uploadQuery = "insert into Labor(LaborId, LaborName, LaborDescription, LaborLink, LaborHours, PartId) " +
            "values(@LaborId, @LaborName, @LaborDescription, @LaborLink, @LaborHours, @PartId)";
        private const string uploadQueryNew = "insert into Labor(LaborId, LaborName, LaborDescription, LaborLink, LaborHours) " +
            "values(@LaborId, @LaborName, @LaborDescription, @LaborLink, @LaborHours)";
        private const string getQuery = "select LaborId, LaborName, LaborDescription, LaborLink, LaborHours " +
            "from Labor " +
            "where LaborId = @LaborId and PartId = @PartId";
        private const string getQueryId = "select top(1) (LaborId+1) As LaborId  " +
            "from Labor " +
            "order by LaborId desc";

        // Fields

        // Properties
        public Labor Labor
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Labor));
            }
        }
        public int LaborId
        {
            get => Item.LaborId;
            set
            {
                Item.LaborId = value;
                OnPropertyChanged(nameof(LaborId));
            }
        }
        public string LaborName
        {
            get => Item.LaborName;
            set
            {
                Item.LaborName = value;
                OnPropertyChanged(nameof(LaborName));
            }
        }
        public string LaborDescription
        {
            get => Item.LaborDescription;
            set
            {
                Item.LaborDescription = value;
                OnPropertyChanged(nameof(LaborDescription));
            }
        }
        public string LaborLink
        {
            get => Item.LaborLink;
            set
            {
                Item.LaborLink = value;
                OnPropertyChanged(nameof(LaborLink));
            }
        }
        public decimal LaborHours
        {
            get => Item.LaborHours;
            set
            {
                Item.LaborHours = value;
                OnPropertyChanged(nameof(LaborHours));
            }
        }

        public ICommand AddPart { get; set; }

        // Constructor
        /// <summary>
        /// Invoke constructor with getting information about labor and using multiply parameters for query from dictionary
        /// </summary>
        /// <param name="labor">Type Labor</param>
        /// <param name="nameIdPairs">Dictionary with multipy parameters for Query</param>
        public LaborWindowViewModel(Labor labor, Dictionary<string, object> nameIdPairs):base(selectQueryId, updateQuery, uploadQuery, getQuery, nameIdPairs)
        {
            AddPart = new ViewModelCommand<object>(ExecuteAddPart);
        }
        public LaborWindowViewModel():base(selectQueryNew, updateQueryNew, uploadQueryNew, getQueryId)
        {
            AddPart = new ViewModelCommand<object>(ExecuteAddPart);
        }
        /// <summary>
        /// Invoke constructor with using multiply parameters for query from dictionary
        /// </summary>
        /// <param name="nameIdPairs">Dictionary with multipy parameters for Query</param>
        public LaborWindowViewModel(Dictionary<string, object> nameIdPairs):base(selectQueryId, updateQuery, uploadQuery, getQueryId, nameIdPairs)
        {
            AddPart = new ViewModelCommand<object>(ExecuteAddPart);
        }

        // Methods
        private void ExecuteAddPart(object obj)// Doesn't work because didn't finish. The main idea in adding Car Parts which is linked to the labor
        {

        }
    }
}
