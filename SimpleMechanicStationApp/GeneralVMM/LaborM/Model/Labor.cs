using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.LaborM.Model
{
    public class Labor:ViewModelBase
    {
        private int _laborId;
        public int LaborId
        {
            get { return _laborId; }
            set
            {
                if (_laborId != value)
                {
                    _laborId = value;
                    OnPropertyChanged(nameof(LaborId));
                }
            }
        }

        private string _laborName;
        public string LaborName
        {
            get { return _laborName; }
            set
            {
                if (_laborName != value)
                {
                    _laborName = value;
                    OnPropertyChanged(nameof(LaborName));
                }
            }
        }

        private decimal _laborHours;
        public decimal LaborHours
        {
            get { return _laborHours; }
            set
            {
                if (_laborHours != value)
                {
                    _laborHours = value;
                    OnPropertyChanged(nameof(LaborHours));
                    OnPropertyChanged(nameof(LaborSoldPrice));
                }
            }
        }

        private string _mechanicName;
        public string MechanicName
        {
            get { return _mechanicName; }
            set
            {
                if (_mechanicName != value)
                {
                    _mechanicName = value;
                    OnPropertyChanged(nameof(MechanicName));
                }
            }
        }

        private decimal _laborSoldPrice;
        public decimal LaborSoldPrice
        {
            get { return Math.Round(LaborHours * 150); }
            set
            {
                if (_laborSoldPrice != value)
                {
                    _laborSoldPrice = value;
                    OnPropertyChanged(nameof(LaborSoldPrice));
                }
            }
        }
    }
}
