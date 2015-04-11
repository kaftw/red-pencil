using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencil
{
    public class Product
    {
        public double MSRP { get; set; }

        public double SalePrice { get; set; }

        public DateTime PreviousPriceChangeOccurredAt { get; set; }
    }
}
