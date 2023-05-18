using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CarVIN { get; set; }
        public int CarId { get; set; }
        public string CarPlate { get; set; }
        public string CarName { get; set; }
        public DateTime CarYear { get; set; }
        public string Summary { get; set; }
    }
}
