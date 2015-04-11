using System;
using System.Collections.Generic;
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
            throw new InvalidOperationException();
        }
    }
}
