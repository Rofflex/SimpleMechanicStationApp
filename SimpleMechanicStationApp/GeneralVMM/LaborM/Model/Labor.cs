using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.LaborM.Model
{
    public class Labor
    {
        public int LaborId { get; set; }
        public string LaborName { get; set; }
        public decimal LaborHours { get; set; }
        public string MechanicName { get; set; }
        public decimal LaborSoldPrice { get; set; }
        public decimal LaborsAmount { get; set; }
    }
}
