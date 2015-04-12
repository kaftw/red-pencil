using System;
using System.ComponentModel;

namespace RedPencil
{
    public class RedPencil
    {
        internal RedPencil()
        {
        }

        public DateTime StartDate { get; internal set; }

        public DateTime EndDate { get; internal set; }

        public double SalePrice { get; private set; }

        public static Boolean IsEligible(Product product)
        {
            var percentageDiscount = (product.MSRP - product.SalePrice) / product.MSRP;
            return percentageDiscount >= .05 && percentageDiscount <= .3 && DateTime.Now.Subtract(product.PreviousPriceChangeOccurredAt).Days >= 30;
        }

        public void Begin(Product product)
        {
            if (!IsEligible(product))
                throw new InvalidOperationException();

            SalePrice = product.SalePrice;
            product.PropertyChanged += OnProductPropertyChanged;
        }

        internal bool Intersects(DateTime startDate, DateTime endDate)
        {
            return (StartDate >= startDate && StartDate <= endDate) || (EndDate >= startDate && EndDate <= endDate);
        }

        private void OnProductPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SalePrice")
            {
                var product = (Product)sender;
                if (SalePrice < product.SalePrice || (product.MSRP - product.SalePrice) / product.MSRP > .3)
                {
                    EndDate = DateTime.Now;
                }
            }
        }
    }
}
