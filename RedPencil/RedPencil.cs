using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPencil
{
    public class RedPencil
    {
        public Boolean IsEligible(Product product)
        {
            var percentageDiscount = (product.MSRP - product.SalePrice) / product.MSRP;
            return percentageDiscount >= .05 && percentageDiscount <= .3 && DateTime.Now.Subtract(product.PreviousPriceChangeOccurredAt).Days >= 30;
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void Begin(Product product)
        {
            if (!IsEligible(product) || (EndDate - StartDate).Days > 30)
                throw new InvalidOperationException();

            SalePrice = product.SalePrice;
            product.PropertyChanged += OnProductPropertyChanged;
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

        public double SalePrice { get; set; }
    }
}
