using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedPencil.Test
{
    [TestClass]
    public class RedPencilTests
    {
        [TestMethod]
        public void RedPencilPromotionTakesEffectAt5PercentDiscount()
        {
            Product product = new Product();
            product.MSRP = 1.0d;
            product.SalePrice = 0.95d;

            RedPencil promotion = new RedPencil();
            Assert.AreEqual(true, promotion.IsEligible(product));
        }
    }
}
