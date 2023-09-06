using SimpleMechanicStationApp.CarPartWindow.View;
using SimpleMechanicStationApp.CarPartWindow.ViewModel;
using SimpleMechanicStationApp.CarWindow.View;
using SimpleMechanicStationApp.CarWindow.ViewModel;
using SimpleMechanicStationApp.CustomerWindow.View;
using SimpleMechanicStationApp.CustomerWindow.ViewModel;
using SimpleMechanicStationApp.DialogWindow.BaseViewModel;
using SimpleMechanicStationApp.DialogWindow.View;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using SimpleMechanicStationApp.GeneralVMM.CarM.Model;
using SimpleMechanicStationApp.GeneralVMM.CustomerM.Model;
using SimpleMechanicStationApp.GeneralVMM.LaborM.Model;
using SimpleMechanicStationApp.GeneralVMM.MechanicM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderM.Model;
using SimpleMechanicStationApp.GeneralVMM.PartM.Model;
using SimpleMechanicStationApp.LaborWindow.View;
using SimpleMechanicStationApp.LaborWindow.ViewModel;
using SimpleMechanicStationApp.MechanicWindow.View;
using SimpleMechanicStationApp.MechanicWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.OrderWindow.ViewModel
{
    public class OrderWindowViewModel : WindowVM<Order,int>
    {
        // Constant
        private const string queryForParts = "select PartOrder.PartId, CarPart.PartName, " +
            "Manufacture.ManufactureName, CarPart.PartRetailPrice, CarPart.PartTradePrice, " +
            "PartOrder.Quantity, PartOrder.PartSoldPrice, PartOrder.ManufactureId " +
            "from [Order] inner join PartOrder on [Order].OrderId=[PartOrder].OrderId " +
            "inner join CarPart on [PartOrder].ManufactureId = CarPart.ManufactureId and [PartOrder].PartId = [CarPart].PartId " +
            "inner join Manufacture on PartOrder.ManufactureId=Manufacture.ManufactureId " +
            "where [Order].OrderId=@id";
        private const string queryForLabors = "select LaborOrder.LaborId, LaborName, LaborHours, MechanicName, " +
            "LaborSoldPrice, LaborsAmount, LaborSoldHours, LaborOrder.MechanicId " +
            "from [Order] inner join LaborOrder on [Order].OrderId=LaborOrder.OrderId " +
            "inner join Labor on LaborOrder.LaborId=Labor.LaborId " +
            "inner join Mechanic on Mechanic.MechanicId=LaborOrder.MechanicId " +
            "where [Order].OrderId=@id";
        private const string getQuery = "select [Order].OrderId, [Order].CarId, CarMake, CarModel,CarOdometerStart, CarOdometerFinish, " +
            "CarPlate, CarYear, [Order].CustomerId, CustomerName, CustomerPhone, CustomerAddress, OrderOpenDate, OrderCloseDate, " +
            "[Order].VIN, (CarMake +' '+ CarModel) as CarName, Discount, Deposit, SalesTax, WasteMaterialFee, WasteMaterialFeeIncluded " +
            "from Customer inner join [Order] on Customer.CustomerId=[Order].CustomerId " +
            "inner join [CarInfo] on [Order].CarId = [CarInfo].CarId " +
            "where OrderId=@Id";
        private const string getQueryId = "select top(1) (OrderId+1) As OrderId  " +
            "from [Order] " +
            "order by OrderId desc";
        private const string queryDialogWindowLabors = "select LaborId, LaborName, LaborHours, CarPart.PartId, PartName " +
           "from Labor " +
           "inner join CarPart on Labor.PartId=CarPart.PartId " +
           "where Labor.PartId = @id " +
           "group by LaborId, LaborName, CarPart.PartId, PartName, LaborHours";
        private const string queryDialogWindowMechanic = "select * from Mechanic";
        private const string queryDialogWindowCustomer = "select * from Customer";
        private const string queryDialogWindowCar = "select CarInfo.CarId, (CarMake +' '+ CarModel) as CarName, " +
            "CarMake, CarModel, CarYear, CarEngine, " +
            "CarBodyStyle, CarTrimLevel, CarWheelDrive, CarTransmission, " +
            "Color, CarAdditional, CarLink " +
            "from CarInfo ";
        private const string queryDialogWindowCarParts = "select CarPart_CarInfo.PartId, PartName, PartRetailPrice, " +
            "PartTradePrice, CarPart_CarInfo.ManufactureId, ManufactureName " +
            "from CarPart_CarInfo " +
            "inner join Manufacture on CarPart_CarInfo.ManufactureId=Manufacture.ManufactureId " +
            "inner join CarPart on CarPart.PartId = CarPart_CarInfo.PartId and CarPart.ManufactureId = CarPart_CarInfo.ManufactureId " +
            "where CarPart_CarInfo.CarId=@id";

        // Fields
        private ObservableCollection<Part> _parts;
        private Part _selectedItemParts;
        private ObservableCollection<Labor> _labors;
        private Labor _selectedItemLabors;

        // Properties
        public Order Order
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Order));
            }
        }
        public ObservableCollection<Part> Parts
        {
            get => _parts;
            set
            {
                _parts = value;
                OnPropertyChanged(nameof(Parts));
            }
        }
        public ObservableCollection<Labor> Labors
        {
            get => _labors; 
            set
            {
                _labors = value;
                OnPropertyChanged(nameof(Labors));
            }
        }
        public int OrderId
        {
            get => Item.OrderId;
            set
            {
                Item.OrderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }
        public string CarMake
        {
            get => Item.CarMake;
            set
            {
                Item.CarMake = value;
                OnPropertyChanged(nameof(CarMake));
            }
        }
        public string CarModel
        {
            get => Item.CarModel;
            set
            {
                Item.CarModel = value;
                OnPropertyChanged(nameof(CarModel));
            }
        }
        public string VIN
        {
            get => Item.VIN;
            set
            {
                Item.VIN = value;
                OnPropertyChanged(nameof(VIN));
            }
        }
        public string CarPlate
        {
            get => Item.CarPlate;
            set
            {
                Item.CarPlate = value;
                OnPropertyChanged(nameof(CarPlate));
            }
        }
        public int CarYear
        {
            get => Item.CarYear;
            set
            {
                Item.CarYear = value;
                OnPropertyChanged(nameof(CarYear));
            }
        }
        public double CarOdometerStart
        {
            get => Item.CarOdometerStart;
            set
            {
                Item.CarOdometerStart = value;
                OnPropertyChanged(nameof(CarOdometerStart));
            }
        }
        public double CarOdometerFinish
        {
            get => Item.CarOdometerFinish;
            set
            {
                Item.CarOdometerFinish = value;
                OnPropertyChanged(nameof(CarOdometerFinish));
            }
        }
        public string CarName
        {
            get => Item.CarName;
            set
            {
                Item.CarName = value;
                OnPropertyChanged(nameof(CarName));
            }
        }
        public string CustomerName
        {
            get => Item.CustomerName;
            set
            {
                Item.CustomerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }
        public string CustomerAddress
        {
            get => Item.CustomerAddress;
            set
            {
                Item.CustomerAddress = value;
                OnPropertyChanged(nameof(CustomerAddress));
            }
        }
        public string CustomerPhone
        {
            get => Item.CustomerPhone;
            set
            {
                Item.CustomerPhone = value;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }
        public string OrderOpenDate
        {
            get
            {
                if (Item.OrderOpenDate != DateTime.MinValue)
                {
                    return Item.OrderOpenDate.ToString("MM.dd.yyyy");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value is null)
                {
                    Item.OrderOpenDate = DateTime.MinValue;
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
                else
                {
                    Item.OrderOpenDate = Convert.ToDateTime(value);
                    OnPropertyChanged(nameof(OrderOpenDate));
                }
            }
        }
        public string OrderCloseDate
        {
            get
            {
                if (Item.OrderCloseDate != DateTime.MinValue)
                {
                    return Item.OrderCloseDate.ToString("MM.dd.yyyy");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value is null)
                {
                    Item.OrderCloseDate = DateTime.MinValue;
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
                else
                {
                    Item.OrderCloseDate = Convert.ToDateTime(value);
                    OnPropertyChanged(nameof(OrderCloseDate));
                }
            }
        }
        public decimal PartsAmount
        {
            get => Item.PartsAmount;
            set
            {
                Item.PartsAmount = value;
                OnPropertyChanged(nameof(PartsAmount));
            }
        }
        public decimal LaborsAmount
        {
            get => Item.LaborsAmount;
            set
            {
                Item.LaborsAmount = value;
                OnPropertyChanged(nameof(LaborsAmount));
            }
        }
        public decimal TaxAmount => Item.TaxAmount;
        public decimal WasteMaterialFee
        {
            get => Item.WasteMaterialFee;
            set
            {
                Item.WasteMaterialFee = value;
                OnPropertyChanged(nameof(WasteMaterialFee));
            }
        }
        public bool WasteMaterialFeeIncluded
        {
            get => Item.WasteMaterialFeeIncluded;
            set
            {
                Item.WasteMaterialFeeIncluded = value;
                OnPropertyChanged(nameof(WasteMaterialFeeIncluded));
                OnPropertyChanged(nameof(OrderAmount));
            }
        }
        public decimal SubletRepair
        {
            get => Item.SubletRepair;
            set
            {
                Item.SubletRepair = value;
                OnPropertyChanged(nameof(SubletRepair));
            }
        }
        public decimal Discount
        {
            get => Item.Discount;
            set
            {
                Item.Discount = value;
                OnPropertyChanged(nameof(Discount));
                OnPropertyChanged(nameof(TaxAmount));
                OnPropertyChanged(nameof(OrderAmount));
            }
        }
        public decimal OrderAmount => Item.OrderAmount;
        public decimal Deposit
        {
            get => Item.Deposit;
            set
            {
                Item.Deposit = value;
                OnPropertyChanged(nameof(Deposit));
            }
        }
        public Part SelectedItemParts 
        {
            get => _selectedItemParts;
            set
            {
                _selectedItemParts = value;
                OnPropertyChanged(nameof(SelectedItemParts));
            }
        }
        public Labor SelectedItemLabors
        {
            get => _selectedItemLabors; 
            set
            {
                _selectedItemLabors = value;
                OnPropertyChanged(nameof(SelectedItemLabors));
            }
        }

        public ICommand DefinateColumnsChanged { get; set; }
        public ICommand AddParts { get; set; }
        public ICommand AddLabors { get; set; }
        public ICommand ChooseCar { get; set; }
        public ICommand ChooseCustomer { get; set; }

        // Constructor
        public OrderWindowViewModel(int orderId):base(orderId, getQuery)
        {
            _parts = new ObservableCollection<Part>(_dbCommands.GetItemsForList<Part, int>(orderId, queryForParts));
            _labors = new ObservableCollection<Labor>(_dbCommands.GetItemsForList<Labor, int>(orderId, queryForLabors));
            UpdateAmount();
            DefinateColumnsChanged = new ViewModelCommand<object>(ExecuteDefinateColumnsChanged);
            Save = new ViewModelCommand<object>(ExecuteSave);
            AddParts = new ViewModelCommand<object>(ExecuteAddParts);
            AddLabors = new ViewModelCommand<object>(ExecuteAddLabors);
            ChooseCar = new ViewModelCommand<object>(ExecuteChooseCar);
            ChooseCustomer = new ViewModelCommand<object>(ExecuteChooseCustomer); 
        }
        public OrderWindowViewModel():base(getQueryId)
        {
            _parts = new ObservableCollection<Part>();
            _labors = new ObservableCollection<Labor>();
            UpdateAmount();
            DefinateColumnsChanged = new ViewModelCommand<object>(ExecuteDefinateColumnsChanged);
            Save = new ViewModelCommand<object>(ExecuteSave);
            AddParts = new ViewModelCommand<object>(ExecuteAddParts);
            AddLabors = new ViewModelCommand<object>(ExecuteAddLabors);
            ChooseCar = new ViewModelCommand<object>(ExecuteChooseCar);
            ChooseCustomer = new ViewModelCommand<object>(ExecuteChooseCustomer);
        }

        // Methods
        /// <summary>
        /// Update amount of Parts and Labors
        /// </summary>
        private void UpdateAmount()
        {
            PartsAmount = GetSummaryOfColumn<Part>(Parts, "Summary");
            LaborsAmount = GetSummaryOfColumn<Labor>(Labors, "LaborSoldPrice");
        }
        private void ExecuteSave(object param)
        {
            if (IsEditing)
            {
                if (CarName is null || CarName == "")
                {
                    MessageBox.Show("You have to choose car");
                }
                else if (CustomerName is null || CustomerName == "") 
                {
                    MessageBox.Show("You have to choose customer");
                }
                else
                {
                    var Result = MessageBox.Show("Do you want to save changes ?", "Save changes", MessageBoxButton.YesNo);
                    if (Result == MessageBoxResult.Yes)
                    {
                        RemoveSelection();
                        SaveOrder(Order, Parts, Labors);
                        IsEditing = false;
                        IsReadOnly = true;
                    }
                    else
                    {
                        //Didn't finish it. It can work with method Clone which is base method of models. You can clone item and if worker doesn't want to save changes return Cloned items
                        IsEditing = true;
                        IsReadOnly = false;
                    }
                }
            }
        }
        /// <summary>
        /// It updates parts and labors amounts plus tex amount and order amount
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteDefinateColumnsChanged(object param)
        {
            UpdateAmount();
            OnPropertyChanged(nameof(TaxAmount));
            OnPropertyChanged(nameof(OrderAmount));
        }
        /// <summary>
        /// 1) Creates DialogWindowVM with a type of item, type of item window viewmodel, item window view, type of item Id
        /// 2) Creates DialogWindowView
        /// 3) Assign dialogWindowVM to dialogWindow DataContext
        /// 4) Opens dialogWindow with a ShowDialog
        /// 5) If double clicked on item in DataGrid in DialogWindow - result = true and then we take selected item from DialogWindow
        /// 6) if this Item has already added in Items list then MessageBox otherwise Add to items list
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteAddParts(object param) 
        {
            if (IsEditing)
            {
                // dictionary to use in CarPartWindow where should be id, CarId, OldPartId and OldManufactureId.
                // It is needed because CarPartWindowViewModel has constructor with query for carParts and these parameters
                var tDictionary = new Dictionary<string, object>
                    {
                        { "id", Order.CarId},
                        { "CarId", Order.CarId},
                        { "OldPartId", null},
                        { "OldManufactureId", null}
                     }; 
                var dialogWindowVM = new DialogWindowVM<Part, CarPartWindowViewModel, CarPartWindowView, string>(queryDialogWindowCarParts, tDictionary);
                DialogWindowView dialogWindow = new DialogWindowView();
                dialogWindow.DataContext = dialogWindowVM;

                bool? result = dialogWindow.ShowDialog();

                if (result == true)
                {
                    var selectedPart = dialogWindowVM.SelectedItem;
                    if (selectedPart is Part) 
                    {
                        if (Parts.Any(p => p.PartId == selectedPart.PartId && p.ManufactureId == selectedPart.ManufactureId))
                        {
                            MessageBox.Show($"Selected part: {selectedPart.PartId} manufacture: {selectedPart.ManufactureName} is already added");
                        }
                        else 
                        {
                            Parts.Add(selectedPart);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// The same actions but with mechanic dialogWindow because If labor added we should choose mechanic
        /// 1) Creates DialogWindowVM with a type of item, type of item window viewmodel, item window view, type of item Id
        /// 2) Creates DialogWindowView
        /// 3) Assign dialogWindowVM to dialogWindow DataContext
        /// 4) Opens dialogWindow with a ShowDialog
        /// 5) If double clicked on item in DataGrid in DialogWindow - result = true and then we take selected item from DialogWindow
        /// 6) if this Item has already added in Items list then MessageBox otherwise Add to items list
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteAddLabors(object param)
        {
            if (IsEditing && SelectedItemParts != null)
            {
                var dialogWindowVMLabor = new DialogWindowVM<Labor, LaborWindowViewModel, LaborWindowView, int>(queryDialogWindowLabors, 
                    new Dictionary<string, object> { 
                        {"PartId", SelectedItemParts.PartId},
                        {"id", SelectedItemParts.PartId},
                        {"LaborId", null}});
                var dialogWindowVMMechanic = new DialogWindowVM<Mechanic, MechanicWindowViewModel, MechanicWindowView, int>(queryDialogWindowMechanic);

                DialogWindowView dialogWindow = new DialogWindowView();
                dialogWindow.DataContext = dialogWindowVMLabor;

                bool? result = dialogWindow.ShowDialog();

                if (result == true)
                {
                    Labor selectedLabor = dialogWindowVMLabor.SelectedItem;
                    if (selectedLabor is Labor)
                    {
                        if (Labors.Any(p => p.LaborId == selectedLabor.LaborId))
                        {
                            MessageBox.Show($"Selected labor: {selectedLabor.LaborId} is already added");
                        }
                        else
                        {
                            MessageBox.Show("Choose mechanic");
                            dialogWindow = new DialogWindowView();
                            dialogWindow.DataContext = dialogWindowVMMechanic;
                            bool? _result = dialogWindow.ShowDialog();
                            if (_result == true)
                            {
                                Mechanic selectedMechanic = dialogWindowVMMechanic.SelectedItem;
                                if (selectedMechanic is Mechanic) 
                                {
                                    selectedLabor.MechanicId = selectedMechanic.MechanicId;
                                    selectedLabor.MechanicName = selectedMechanic.MechanicName;
                                    Labors.Add(selectedLabor);
                                }
                            }
                        }
                    }
                }
            }
            else 
            {
                MessageBox.Show("Choose car part to add labor");
            }
        }
        /// <summary>
        /// Get summary of column from collection. For example it is used for Parts amount or Labors amount
        /// </summary>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <param name="Collection">Collection where should get summary</param>
        /// <param name="ColumnName">Column name in DataGrid</param>
        /// <returns>summary of rows in columnName</returns>
        private decimal GetSummaryOfColumn<T>(ObservableCollection<T> collection, string columnName)
        {
            decimal summary = 0;
            if (collection.Count != 0)
            {
                Type type = collection[0].GetType();
                PropertyInfo ?propertyInfo = type.GetProperty(columnName);
                foreach (var item in collection)
                {
                    if (propertyInfo is not null)
                    {
                        decimal columnValue = (decimal)propertyInfo.GetValue(item);
                        summary += columnValue;
                    }
                }
            }
            return summary;
        }
        /// <summary>
        /// Remove selection from parts and labors DataGrid
        /// </summary>
        private void RemoveSelection() 
        {
            SelectedItemLabors = null;
            SelectedItemParts = null;
        }
        /// <summary>
        /// 1) Creates DialogWindowVM with a type of item, type of item window viewmodel, item window view, type of item Id
        /// 2) Creates DialogWindowView
        /// 3) Assign dialogWindowVM to dialogWindow DataContext
        /// 4) Opens dialogWindow with a ShowDialog
        /// 5) If double clicked on item in DataGrid in DialogWindow - result = true and then we take selected item from DialogWindow
        /// 6) if this Item has already added in Items list then MessageBox otherwise Add to items list
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteChooseCar(object param)
        {
            if (IsEditing)
            {
                var dialogWindowVM = new DialogWindowVM<Car, CarWindowViewModel, CarWindowView, int>(queryDialogWindowCar);
                DialogWindowView dialogWindow = new DialogWindowView();
                dialogWindow.DataContext = dialogWindowVM;

                bool? result = dialogWindow.ShowDialog();

                if (result == true)
                {
                    var selectedCar = dialogWindowVM.SelectedItem;
                    if (selectedCar is Car)
                    {
                        if (Order.CarId == selectedCar.CarId)
                        {
                            MessageBox.Show($"Selected car: {selectedCar.CarName} is already added");
                        }
                        else
                        {
                            Order.CarId = selectedCar.CarId;
                            CarName = selectedCar.CarName;
                            CarMake = selectedCar.CarMake;
                            CarModel = selectedCar.CarModel;
                            CarYear = selectedCar.CarYear;
                            VIN = selectedCar.VIN;
                            CarOdometerStart = 0;
                            CarOdometerFinish = 0;
                            CarPlate = "";
                            OrderOpenDate = null;
                            OrderCloseDate = null;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 1) Creates DialogWindowVM with a type of item, type of item window viewmodel, item window view, type of item Id
        /// 2) Creates DialogWindowView
        /// 3) Assign dialogWindowVM to dialogWindow DataContext
        /// 4) Opens dialogWindow with a ShowDialog
        /// 5) If double clicked on item in DataGrid in DialogWindow - result = true and then we take selected item from DialogWindow
        /// 6) if this Item has already added in Items list then MessageBox otherwise Add to items list
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteChooseCustomer(object param) 
        {
            if (IsEditing)
            {
                var dialogWindowVM = new DialogWindowVM<Customer, CustomerWindowViewModel, CustomerWindowView, int>(queryDialogWindowCustomer);

                DialogWindowView dialogWindow = new DialogWindowView();
                dialogWindow.DataContext = dialogWindowVM;

                bool? result = dialogWindow.ShowDialog();

                if (result == true)
                {
                    var selectedCustomer = dialogWindowVM.SelectedItem;
                    if (selectedCustomer is Customer)
                    {
                        if (Order.CustomerId == selectedCustomer.CustomerId)
                        {
                            MessageBox.Show($"Selected customer: {selectedCustomer.CustomerName} is already added");
                        }
                        else
                        {
                            Order.CustomerId = selectedCustomer.CustomerId;
                            CustomerName = selectedCustomer.CustomerName;
                            CustomerAddress = selectedCustomer.CustomerAddress;
                            CustomerPhone = selectedCustomer.CustomerPhone;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 1) Gets connection from _dbCommands.GetConnection()
        /// 2) Select the same orderId from table order
        /// 3) If any order with orderId found, then use CompareAndUpdateOrder procedure from DB otherwise insert new order to the DB
        /// 4) if parts count is not 0 delete parts list from PartOrder and insert new list because it is faster than check and compare every item
        /// 5) if labors count is not 0 delete labors list from laborOrder and insert new list because it is faster than check and compare every item
        /// It works with transaction. If any errors then transaction will be Rollbacked 
        /// </summary>
        /// <param name="order">Type Order</param>
        /// <param name="parts">Collection parts</param>
        /// <param name="labors">Collection labors</param>
        /// <returns>0 - no connection, 1 - DB exception, 2 - queries executed</returns>
        private int SaveOrder(Order order, ObservableCollection<Part> parts, ObservableCollection<Labor> labors)
        {
            int flag = 0;

            using (SqlConnection con = _dbCommands.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    if (order != null)
                    {
                        object result;
                        string selectCommandText = "select OrderId from [Order] where OrderId = @OrderId";
                        using (SqlCommand selectCommand = new SqlCommand(selectCommandText, con, transaction))
                        {
                            selectCommand.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                            result = selectCommand.ExecuteScalar();
                        }
                        if (result is not null)
                        {
                            using (SqlCommand uploadCommand = new SqlCommand("CompareAndUpdateOrder", con, transaction))
                            {
                                uploadCommand.CommandType = System.Data.CommandType.StoredProcedure;

                                uploadCommand.Parameters.Clear();
                                uploadCommand.Parameters.Add("@OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                                uploadCommand.Parameters.Add("@CarId", System.Data.SqlDbType.Int).Value = order.CarId;
                                uploadCommand.Parameters.Add("@CarodometerStart", System.Data.SqlDbType.Float).Value = order.CarOdometerStart;
                                uploadCommand.Parameters.Add("@CarodometerFinish", System.Data.SqlDbType.Float).Value = order.CarOdometerFinish;
                                uploadCommand.Parameters.Add("@CarPlate", System.Data.SqlDbType.VarChar).Value = order.CarPlate;
                                uploadCommand.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = order.CustomerId;
                                uploadCommand.Parameters.Add("@OrderOpenDate", System.Data.SqlDbType.Date).Value = order.OrderOpenDate;
                                uploadCommand.Parameters.Add("@OrderCloseDate", System.Data.SqlDbType.Date).Value = order.OrderCloseDate;
                                uploadCommand.Parameters.Add("@OrderAmount", System.Data.SqlDbType.Decimal).Value = order.OrderAmount;
                                uploadCommand.Parameters.Add("@PartsAmount", System.Data.SqlDbType.Decimal).Value = order.PartsAmount;
                                uploadCommand.Parameters.Add("@LaborsAmount", System.Data.SqlDbType.Decimal).Value = order.LaborsAmount;
                                uploadCommand.Parameters.Add("@Discount", System.Data.SqlDbType.Decimal).Value = order.Discount;
                                uploadCommand.Parameters.Add("@WasteMaterialFeeIncluded", System.Data.SqlDbType.Bit).Value = order.WasteMaterialFeeIncluded;
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string uploadCommandText = "insert into [Order](OrderId, CarId, CarodometerStart, CarodometerFinish, CarPlate, CustomerId, OrderOpenDate, OrderCloseDate, OrderAmount, PartsAmount, LaborsAmount, Discount, WasteMaterialFeeIncluded) " +
                                "values(@OrderId, @CarId, @CarodometerStart, @CarodometerFinish, @CarPlate, @CustomerId, @OrderOpenDate, @OrderCloseDate, @OrderAmount, @PartsAmount, @LaborsAmount, @Discount, @WasteMaterialFeeIncluded)";
                            using (SqlCommand uploadCommand = new SqlCommand(uploadCommandText, con, transaction))
                            {
                                uploadCommand.Parameters.Clear();
                                uploadCommand.Parameters.Add("@OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                                uploadCommand.Parameters.Add("@CarId", System.Data.SqlDbType.Int).Value = order.CarId;
                                uploadCommand.Parameters.Add("@CarodometerStart", System.Data.SqlDbType.Float).Value = order.CarOdometerStart;
                                uploadCommand.Parameters.Add("@CarodometerFinish", System.Data.SqlDbType.Float).Value = order.CarOdometerFinish;
                                uploadCommand.Parameters.Add("@CarPlate", System.Data.SqlDbType.VarChar).Value = order.CarPlate;
                                uploadCommand.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = order.CustomerId;
                                uploadCommand.Parameters.Add("@OrderOpenDate", System.Data.SqlDbType.Date).Value = order.OrderOpenDate;
                                uploadCommand.Parameters.Add("@OrderCloseDate", System.Data.SqlDbType.Date).Value = order.OrderCloseDate;
                                uploadCommand.Parameters.Add("@OrderAmount", System.Data.SqlDbType.Decimal).Value = order.OrderAmount;
                                uploadCommand.Parameters.Add("@PartsAmount", System.Data.SqlDbType.Decimal).Value = order.PartsAmount;
                                uploadCommand.Parameters.Add("@LaborsAmount", System.Data.SqlDbType.Decimal).Value = order.LaborsAmount;
                                uploadCommand.Parameters.Add("@Discount", System.Data.SqlDbType.Decimal).Value = order.Discount;
                                uploadCommand.Parameters.Add("@WasteMaterialFeeIncluded", System.Data.SqlDbType.Bit).Value = order.WasteMaterialFeeIncluded;
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    if (parts.Count != 0)
                    {
                        string deleteCommandText = "delete from PartOrder where OrderId=@OrderId";
                        string uploadCommandText = "insert into PartOrder(OrderId, PartId, ManufactureId, Quantity, PartSoldPrice) " +
                                            "values(@OrderId, @PartId, @ManufactureId, @Quantity, @PartSoldPrice)";
                        using (SqlCommand deleteRows = new SqlCommand(deleteCommandText, con, transaction))
                        {
                            deleteRows.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                            deleteRows.ExecuteNonQuery();
                        }

                        using (SqlCommand uploadCommand = new SqlCommand(uploadCommandText, con, transaction))
                        {
                            foreach (Part part in parts)
                            {
                                uploadCommand.Parameters.Clear();
                                uploadCommand.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                                uploadCommand.Parameters.Add("PartId", System.Data.SqlDbType.VarChar).Value = part.PartId;
                                uploadCommand.Parameters.Add("ManufactureId", System.Data.SqlDbType.Int).Value = part.ManufactureId;
                                uploadCommand.Parameters.Add("PartSoldPrice", System.Data.SqlDbType.Decimal).Value = part.PartSoldPrice;
                                uploadCommand.Parameters.Add("Quantity", System.Data.SqlDbType.Decimal).Value = part.Quantity;
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    if (labors.Count != 0)
                    {
                        string deleteCommandText = "delete from LaborOrder where OrderId=@OrderId";
                        string uploadCommandText = "insert into LaborOrder(OrderId, LaborId, MechanicId, LaborSoldPrice, LaborSoldHours) " +
                                            "values(@OrderId, @LaborId, @MechanicId, @LaborSoldPrice, @LaborSoldHours)";
                        using (SqlCommand deleteRows = new SqlCommand(deleteCommandText, con, transaction))
                        {
                            deleteRows.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                            deleteRows.ExecuteNonQuery();
                        }

                        using (SqlCommand uploadCommand = new SqlCommand(uploadCommandText, con, transaction))
                        {
                            foreach (Labor labor in labors)
                            {
                                uploadCommand.Parameters.Clear();
                                uploadCommand.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = order.OrderId;
                                uploadCommand.Parameters.Add("LaborId", System.Data.SqlDbType.VarChar).Value = labor.LaborId;
                                uploadCommand.Parameters.Add("MechanicId", System.Data.SqlDbType.Int).Value = labor.MechanicId;
                                uploadCommand.Parameters.Add("LaborSoldPrice", System.Data.SqlDbType.Decimal).Value = labor.LaborSoldPrice;
                                uploadCommand.Parameters.Add("LaborSoldHours", System.Data.SqlDbType.Decimal).Value = labor.LaborSoldHours;
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    flag = 1;
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally { con.Close(); }
            }
            return flag;
        }

    }
}

