using SimpleMechanicStationApp.DialogWindow.BaseViewModel;
using SimpleMechanicStationApp.DialogWindow.View;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using SimpleMechanicStationApp.GeneralVMM.ManufactureM.Model;
using SimpleMechanicStationApp.GeneralVMM.PartM.Model;
using SimpleMechanicStationApp.ManufactureWindow.View;
using SimpleMechanicStationApp.ManufactureWindow.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.CarPartWindow.ViewModel
{
    public class CarPartWindowViewModel:WindowVM<Part,string>
    {
        // Constant
        private const string selectQueryId = "select CarPart.PartId, CarPart.ManufactureId, CarPart_CarInfo.CarId " +
            "from CarPart_CarInfo " +
            "inner join CarPart on CarPart.PartId = CarPart_CarInfo.PartId and CarPart.ManufactureId = CarPart_CarInfo.ManufactureId " +
            "where CarPart.PartId = @OldPartId and CarPart.ManufactureId = @OldManufactureId and CarPart_CarInfo.CarId = @CarId";
        private const string selectQuery = "select CarPart.PartId, CarPart.ManufactureId, CarPart_CarInfo.CarId " +
            "from CarPart_CarInfo " +
            "inner join CarPart on CarPart.PartId = CarPart_CarInfo.PartId and CarPart.ManufactureId = CarPart_CarInfo.ManufactureId " +
            "where CarPart.PartId = @PartId and CarPart.ManufactureId = @ManufactureId";
        private const string updateQuery = "insert into CarPart(PartId, PartName, PartDescription, PartLink, PartRetailPrice, PartTradePrice, ManufactureId) " +
            "values(@PartId, @PartName, @PartDescription, @PartLink, @PartRetailPrice, @PartTradePrice, @ManufactureId); " +
            "insert into CarPart_CarInfo(PartId, ManufactureId, CarId) " +
            "values(@PartId, @ManufactureId, @CarId); "+
            "delete from CarPart_CarInfo where ManufactureId = @OldManufactureId and PartId = @OldPartId; " +
            "delete from PartOrder where ManufactureId = @OldManufactureId and PartId = @OldPartId; " +
            "delete from CarPart where PartId = @OldPartId and ManufactureId = @OldManufactureId; ";
        private const string uploadQuery = "insert into CarPart(PartId, PartName, PartDescription, PartLink, PartRetailPrice, PartTradePrice, ManufactureId) " +
            "values(@PartId, @PartName, @PartDescription, @PartLink, @PartRetailPrice, @PartTradePrice, @ManufactureId); " +
            "insert into CarPart_CarInfo(PartId, ManufactureId, CarId) " +
            "values(@PartId, @ManufactureId, @CarId)";
        private const string uploadQuerySecond = "insert into CarPart(PartId, PartName, PartDescription, PartLink, PartRetailPrice, PartTradePrice, ManufactureId) " +
            "values(@PartId, @PartName, @PartDescription, @PartLink, @PartRetailPrice, @PartTradePrice, @ManufactureId); " +
            "if exists(select CarId from CarInfo where CarId = @CarId) begin " +
            "insert into CarPart_CarInfo(PartId, ManufactureId, CarId) " +
            "values(@PartId, @ManufactureId, @CarId) end";
        private const string getQuery = "select PartId, PartName, PartDescription, PartLink, " +
            "PartRetailPrice, PartTradePrice, CarPart.ManufactureId, ManufactureName " +
            "from CarPart " +
            "inner join Manufacture on CarPart.ManufactureId = Manufacture.ManufactureId " +
            "where PartId = @PartId and CarPart.ManufactureId = @ManufactureId";
        private const string queryForManufactures = "select * from Manufacture";

        // Fields

        // Properties
        public Part Part
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Part));
            }
        }
        public string PartId
        {
            get => Item.PartId;
            set
            {
                Item.PartId = value;
                OnPropertyChanged(nameof(PartId));
            }
        }
        public string PartName
        {
            get => Item.PartName;
            set
            {
                Item.PartName = value;
                OnPropertyChanged(nameof(PartName));
            }
        }
        public string PartDescription
        {
            get => Item.PartDescription;
            set
            {
                Item.PartDescription = value;
                OnPropertyChanged(nameof(PartDescription));
            }
        }
        public string PartLink
        {
            get => Item.PartLink;
            set
            {
                Item.PartLink = value;
                OnPropertyChanged(nameof(PartLink));
            }
        }
        public decimal PartRetailPrice
        {
            get => Item.PartRetailPrice;
            set
            {
                Item.PartRetailPrice = value;
                OnPropertyChanged(nameof(PartRetailPrice));
            }
        }
        public decimal PartTradePrice
        {
            get => Item.PartTradePrice;
            set
            {
                Item.PartTradePrice = value;
                OnPropertyChanged(nameof(PartTradePrice));
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

        public ICommand ChooseCars { get; set; }
        public ICommand ChooseManufacture { get; set; }

        // Constructor
        /// <summary>
        /// Invoke constructor with filling out dictionary and assigning save command
        /// </summary>
        /// <param name="part">Type Part</param>
        /// <param name="nameIdPair">Dictionary with multipy parameters for Query</param>
        public CarPartWindowViewModel(Part part, Dictionary<string, object> nameIdPair):base(part)  
        {
            _nameIdPairs = FillDictionary(nameIdPair);
            Save = new ViewModelCommand<object>(ExecuteSave);
            ChooseCars = new ViewModelCommand<object>(ExecuteChooseCars);
            ChooseManufacture = new ViewModelCommand<object>(ExecuteChooseManufacture);
        }
        /// <summary>
        /// Base constructor
        /// </summary>
        public CarPartWindowViewModel() :base()
        {
            ChooseCars = new ViewModelCommand<object>(ExecuteChooseCars);
            ChooseManufacture = new ViewModelCommand<object>(ExecuteChooseManufacture);
        }
        /// <summary>
        /// Invoke constructor with filling out dictionary
        /// </summary>
        /// <param name="nameIdPair">Dictionary with multipy parameters for Query</param>
        public CarPartWindowViewModel(Dictionary<string, object> nameIdPair) : base(selectQuery, updateQuery, uploadQuerySecond, nameIdPair)
        {
            ChooseCars = new ViewModelCommand<object>(ExecuteChooseCars);
            ChooseManufacture = new ViewModelCommand<object>(ExecuteChooseManufacture);
        }

        // Methods
        /// <summary>
        /// Check PartId and ManufactureName if it is null or empty then messagebox with require to fill out them
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteSave(object obj)
        {
            int flag = 0;
            var resultMB = MessageBox.Show("Do you want to save changes ?", "Save item", MessageBoxButton.YesNo);
            if (resultMB == MessageBoxResult.Yes)
            {
                if (PartId is not null && PartId != "")
                {
                    if (ManufactureName is not null && ManufactureName != "")
                    {
                        flag = _dbCommands.SaveItem(Part, selectQueryId, updateQuery, uploadQuery, _nameIdPairs);
                        
                    }
                    else
                    {
                        MessageBox.Show("No value in Manufacture.\nChoose Manufacture");
                        flag = 1;
                    }
                }
                else
                {
                    MessageBox.Show("No value in ID.\nAdd value to ID");
                    flag = 1;
                }
            }
            if (flag == 2)
            {
                PrevItem = (Part)Item.Clone();
                IsEditing = false;
                IsReadOnly = true;
            }
        }
        /// <summary>
        /// Didn't finish it. The main idea is choose cars for CarPart.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteChooseCars(object obj) 
        {
            
        }
        /// <summary>
        /// 1) Creates DialogWindowVM with a type of item, type of item window viewmodel, item window view, type of item Id
        /// 2) Creates DialogWindowView
        /// 3) Assign dialogWindowVM to dialogWindow DataContext
        /// 4) Opens dialogWindow with a ShowDialog
        /// 5) If double clicked on item in DataGrid in DialogWindow - result = true and then we take selected item from DialogWindow
        /// 6) if this Item has already added in Items list then MessageBox otherwise Add to items list
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteChooseManufacture(object obj)
        {
            if (IsEditing)
            {
                var dialogWindowVM = new DialogWindowVM<Manufacture, ManufactureWindowViewModel, ManufactureWindowView, int>(queryForManufactures);
                DialogWindowView dialogWindow = new DialogWindowView();
                dialogWindow.DataContext = dialogWindowVM;

                bool? result = dialogWindow.ShowDialog();

                if (result == true)
                {
                    var selectedManufacture = dialogWindowVM.SelectedItem;
                    if (selectedManufacture is Manufacture)
                    {
                        if (Part.ManufactureId == selectedManufacture.ManufactureId)
                        {
                            MessageBox.Show($"Selected manufacture: {selectedManufacture.ManufactureName} is already added");
                        }
                        else
                        {
                            Part.ManufactureId = selectedManufacture.ManufactureId;
                            ManufactureName = selectedManufacture.ManufactureName;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Fills out dictionary with parameters OldPartId and OldManufatureId. These parameters are needed for insert query to update items
        /// </summary>
        /// <param name="nameIdPair">Dictionary to fill out</param>
        /// <returns>returns filled out dictionary</returns>
        private Dictionary<string, object> FillDictionary(Dictionary<string,object>nameIdPair) 
        {
            if (nameIdPair.ContainsKey("OldPartId")) 
            { 
                nameIdPair["OldPartId"] = PartId; 
            }
            else 
            { 
                nameIdPair.Add("OldPartId", PartId); 
            }

            if (nameIdPair.ContainsKey("OldManufactureId"))
            { 
                nameIdPair["OldManufactureId"] = Part.ManufactureId; 
            }
            else 
            { 
                nameIdPair.Add("OldManufactureId", Part.ManufactureId); 
            }
            return nameIdPair;
        }
    }
}
