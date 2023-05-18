﻿using SimpleMechanicStationApp.GeneralMethods.ViewModelBase;
using SimpleMechanicStationApp.GeneralVMM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralVMM.ViewModel
{
    public class OrderViewModel:ViewModelBase
    {
        public OrderViewModel(Order order) 
        {
            Order = order;
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            string Summary = $"{Order.CarName} {Order.CarVIN} {Order.CarYear.Year} {Order.CarPlate}";
            Order.Summary = Summary;
        }

        public Order Order { get; set; }

        public string Summary => Order.Summary;
    }
}
