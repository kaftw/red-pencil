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
            return percentageDiscount <= .3;
        }
    }
}
