using System;

namespace SimpleMechanicStationApp.GeneralVMM.OrderM.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string VIN { get; set; }
        public string CarPlate { get; set; }
        public int CarYear { get; set; }
        public double CarOdometerStart { get; set; }
        public double CarOdometerFinish { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderOpenDate { get; set; }
        public DateTime OrderCloseDate { get; set; }
        public string CarName { get; set; }
    }
}
