using System;
using System.ComponentModel;

namespace RedPencil
{
    public class Product : INotifyPropertyChanged
    {
        private double salePrice;
        
        public double MSRP { get; set; }

        public double SalePrice 
        {
            get { return salePrice; }
            set 
            {
                if (salePrice != value)
                {
                    salePrice = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("SalePrice"));
                }
            }
        }

        public DateTime PreviousPriceChangeOccurredAt { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
