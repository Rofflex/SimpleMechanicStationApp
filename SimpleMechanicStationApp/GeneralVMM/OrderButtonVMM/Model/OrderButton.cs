using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.Model
{
    public class OrderButton
    {
        public int OrderId{ get; set; }
        public string Summary { get; set; }
        public bool IsEnabled { get; set; } 
    }
}
