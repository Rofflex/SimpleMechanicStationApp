using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using SimpleMechanicStationApp.GeneralVMM.CarM.Model;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace SimpleMechanicStationApp.CarWindow.ViewModel
{
    public class CarWindowViewModel : WindowVM<Car, int>
    {
        // Constant
        private const string selectQueryId = "select CarId from CarInfo where CarId = @CarId; ";
        private const string selectQuery = "select CarMake, CarModel, CarYear, CarEngine, CarBodyStyle, CarTrimLevel, CarWheelDrive, CarTransmission, Color " +
            "from CarInfo " +
            "where CarMake = @CarMake and CarModel = @CarModel and CarYear = @CarYear and CarEngine = @CarEngine and " +
            "CarBodyStyle = @CarBodyStyle and CarTrimLevel = @CarTrimLevel and CarWheelDrive = @CarWheelDrive and CarTransmission = @CarTransmission and Color = @Color;";
        private const string updateQuery = "update CarInfo " +
            "set CarMake = @CarMake, CarModel = @CarModel, CarYear = @CarYear, CarEngine = @CarEngine, " +
            "CarBodyStyle = @CarBodyStyle, CarTrimLevel = @CarTrimLevel, CarWheelDrive = @CarWheelDrive, " +
            "CarTransmission = @CarTransmission, Color = @Color, CarAdditional = @CarAdditional, " +
            "CarLink = @CarLink where CarId = @CarId;";
        private const string uploadQuery = "insert into CarInfo(CarId, CarMake, CarModel, CarYear, CarEngine, " +
            "CarBodyStyle, CarTrimLevel, CarWheelDrive, CarTransmission, Color, CarAdditional, CarLink) " +
            "values(@CarId, @CarMake, @CarModel, @CarYear, @CarEngine, @CarBodyStyle, @CarTrimLevel, @CarWheelDrive, @CarTransmission, @Color, @CarAdditional, @CarLink)";
        private const string getQuery = "select CarId, CarMake, CarModel, CarYear, " +
            "CarEngine, CarBodyStyle, CarTrimLevel, CarWheelDrive, " +
            "CarTransmission, Color, CarAdditional, CarLink " +
            "from CarInfo " +
            "where CarId = @Id";
        private const string getQueryId = "select top(1) (CarId+1) As CarId  " +
            "from CarInfo " +
            "order by CarId desc";

        // Fields
        private string _carVin;

        // Properties
        public Car Car
        {
            get => Item;
            set
            {
                Item = value;
                OnPropertyChanged(nameof(Car));
            }
        }
        public int CarId
        {
            get => Item.CarId;
            set
            {
                Item.CarId = value;
                OnPropertyChanged(nameof(CarId));
            }
        }
        public string CarMake
        {
            get => Item.CarMake;
            set
            {
                Item.CarMake = value;
                Item.CarName = $"{value} {CarModel}";
                OnPropertyChanged(nameof(CarMake));
            }
        }
        public string CarModel
        {
            get => Item.CarModel;
            set
            {
                Item.CarModel = value;
                Item.CarName = $"{CarMake} {value}";
                OnPropertyChanged(nameof(CarModel));
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
        public string CarEngine
        {
            get => Item.CarEngine;
            set
            {
                Item.CarEngine = value;
                OnPropertyChanged(nameof(CarEngine));
            }
        }
        public string CarBodyStyle
        {
            get => Item.CarBodyStyle;
            set
            {
                Item.CarBodyStyle = value;
                OnPropertyChanged(nameof(CarBodyStyle));
            }
        }
        public string CarTrimLevel
        {
            get => Item.CarTrimLevel;
            set
            {
                Item.CarTrimLevel = value;
                OnPropertyChanged(nameof(CarTrimLevel));
            }
        }
        public string CarWheelDrive
        {
            get => Item.CarWheelDrive;
            set
            {
                Item.CarWheelDrive = value;
                OnPropertyChanged(nameof(CarWheelDrive));
            }
        }
        public string CarTransmission
        {
            get => Item.CarTransmission;
            set
            {
                Item.CarTransmission = value;
                OnPropertyChanged(nameof(CarTransmission));
            }
        }
        public string Color
        {
            get => Item.Color;
            set
            {
                Item.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        public string CarAdditional
        {
            get => Item.CarAdditional;
            set
            {
                Item.CarAdditional = value;
                OnPropertyChanged(nameof(CarAdditional));
            }
        }
        public string CarLink
        {
            get => Item.CarLink;
            set
            {
                Item.CarLink = value;
                OnPropertyChanged(nameof(CarLink));
            }
        }
        public string CarVin
        {
            get => _carVin;
            set
            {
                _carVin = value;
                OnPropertyChanged(nameof(CarVin));
            }
        }
        public ICommand Download { get; set; }

        // Constructor
        /// <summary>
        /// Invoke constructor with getting information about car
        /// </summary>
        /// <param name="carId">is chosen car</param>
        public CarWindowViewModel(int carId) : base(carId, selectQueryId, selectQuery, updateQuery, uploadQuery, getQuery)
        {
            Download = new ViewModelCommand<object>(ExecuteDownload);
        }
        /// <summary>
        /// Invoke constructor without any parameters to create new car
        /// </summary>
        public CarWindowViewModel() : base(selectQueryId, selectQuery, updateQuery, uploadQuery, getQueryId)
        {
            Download = new ViewModelCommand<object>(ExecuteDownload);
        }

        // Methods
        private async void ExecuteDownload(object obj)
        {
            if (CarVin != null || CarVin != "") //To test: 3VV4B7AX3NM139593
            {

                // URL from api
                string apiUrl = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinValues/{CarVin}?format=xml";

                using (HttpClient httpClient = new HttpClient())
                {

                    try
                    {
                        // Post-query 
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            // Success
                            string responseBody = await response.Content.ReadAsStringAsync();

                            var xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(responseBody);

                            CarMake = xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/Make")?.InnerText;
                            CarModel = xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/Model")?.InnerText;
                            CarYear = Convert.ToInt32(xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/ModelYear")?.InnerText);
                            CarTransmission = xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/TransmissionStyle")?.InnerText;
                            CarTrimLevel = xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/Trim")?.InnerText;
                            CarEngine = xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/DisplacementL")?.InnerText;
                            CarBodyStyle = xmlDoc.SelectSingleNode("/Response/Results/DecodedVINValues/BodyClass")?.InnerText;

                            MessageBox.Show("Success");
                        }
                        else
                        {
                            // Nothing found
                            MessageBox.Show("Error: " + response.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            
        }
    }
} 
        
