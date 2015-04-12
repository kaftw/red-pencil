using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RedPencil.Test
{
    [TestClass]
    public class RedPencilTests
    {
        [TestMethod]
        public void PromotionTakesEffectAt5PercentDiscount()
        {
            var product = new Product();
            product.MSRP = 1.0d;
            product.SalePrice = 0.95d;

            Assert.AreEqual(true, RedPencil.IsEligible(product));
        }

        [TestMethod]
        public void PromotionDoesNotTakeEffectOver30PercentDiscount()
        {
            var product = new Product();
            product.MSRP = 1;
            product.SalePrice = .65;

            Assert.AreEqual(false, RedPencil.IsEligible(product));
        }

        [TestMethod]
        public void PromotionDoesNotEffectBelow5PercentDiscount()
        {
            var product = new Product();
            product.MSRP = 1;
            product.SalePrice = .96;

            Assert.AreEqual(false, RedPencil.IsEligible(product));
        }

        [TestMethod]
        public void PromotionTakesEffectIfPriceHasNotChangedFor30Days()
        {
            var product = CreateTestProduct();

            Assert.AreEqual(true, RedPencil.IsEligible(product));
        }

        [TestMethod]
        public void PromotionDoesNotTakeEffectWithin30DaysOfPreviousPriceChange()
        {
            var product = CreateTestProduct();
            product.PreviousPriceChangeOccurredAt = DateTime.Now;

            Assert.AreEqual(false, RedPencil.IsEligible(product));
        }        

        private static Product CreateTestProduct()
        {
            var product = new Product();
            product.MSRP = 1;
            product.SalePrice = .9;
            product.PreviousPriceChangeOccurredAt = DateTime.Now.Subtract(TimeSpan.FromDays(30));
            return product;
        }
    }
}
