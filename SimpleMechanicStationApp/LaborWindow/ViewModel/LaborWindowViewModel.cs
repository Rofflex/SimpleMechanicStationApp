using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Windows.Input;
using System.Windows;
using SimpleMechanicStationApp.GeneralVMM.LaborM.Model;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using SimpleMechanicStationApp.GeneralVMM.ManufactureM.Model;
using System.Collections.Generic;

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
        public LaborWindowViewModel(int laborId, string partId):
            base(selectQueryId, updateQuery, uploadQuery, getQuery, new Dictionary<string, object> { { "LaborId", laborId }, { "PartId", partId } })
        {
            AddPart = new ViewModelCommand<object>(ExecuteAddPart);
        }
        public LaborWindowViewModel():base(selectQueryNew, updateQueryNew, uploadQueryNew, getQueryId)
        {
            AddPart = new ViewModelCommand<object>(ExecuteAddPart);
        }

        // Methods
        private void ExecuteAddPart(object obj)
        {

        }
    }
}
