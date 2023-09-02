using System;

namespace SimpleMechanicStationApp.GeneralVMM.OrderM.Model
{
    public class Order:BaseModel<int>
    {
        // Properties
        public int OrderId 
        {
            get => Id;
            set 
            {
                Id = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }
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
        public decimal SalesTax { get; set; }
        public decimal PartsAmount { get; set; }
        public decimal TaxAmount => PartsAmount * GetDiscount() * GetSalesTax();
        public decimal WasteMaterialFee { get; set; }
        public bool WasteMaterialFeeIncluded { get; set; }
        public decimal LaborsAmount { get; set; }
        public decimal SubletRepair { get; set; }
        public decimal OrderAmount => Math.Round(LaborsAmount + (PartsAmount * GetDiscount()) + TaxAmount + (WasteMaterialFeeIncluded?WasteMaterialFee:0) - Deposit, 2);
        public decimal Deposit { get; set; }
        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
        private decimal GetDiscount() 
        {
            return (Discount == 0) ? 1 : (1 - (Discount / 100));
        }
        private decimal GetSalesTax() 
        {
            return SalesTax > 0 ? SalesTax / 100 : 0;
        }
    }
}
