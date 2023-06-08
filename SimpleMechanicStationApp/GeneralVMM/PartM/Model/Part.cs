using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.PartM.Model
{
    public class Part
    {
        public string PartId { get; set; }
        public string PartDescription { get; set; }
        public string ManufactureName { get; set; }
        public decimal PartRetailPrice { get; set; }
        public decimal PartTradePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal PartSoldPrice { get; set; }
        public decimal PartsAmount { get; set; }
    }
}
