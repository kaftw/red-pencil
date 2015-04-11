using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedPencil.Test
{
    [TestClass]
    public class RedPencilTests
    {
        [TestMethod]
        public void PromotionTakesEffectAt5PercentDiscount()
        {
            Product product = new Product();
            product.MSRP = 1.0d;
            product.SalePrice = 0.95d;

            RedPencil promotion = new RedPencil();
            Assert.AreEqual(true, promotion.IsEligible(product));
        }

        [TestMethod]
        public void PromotionDoesNotTakeEffectOver30PercentDiscount()
        {
            Product product = new Product();
            product.MSRP = 1;
            product.SalePrice = .65;

            RedPencil promotion = new RedPencil();
            Assert.AreEqual(false, promotion.IsEligible(product));
        }

        [TestMethod]
        public void PromotionDoesNotEffectBelow5PercentDiscount()
        {
            Product product = new Product();
            product.MSRP = 1;
            product.SalePrice = .96;

            RedPencil promotion = new RedPencil();
            Assert.AreEqual(false, promotion.IsEligible(product));
        }

        [TestMethod]
        public void PromotionTakesEffectIfPriceHasNotChangedFor30Days()
        {
            Product product = new Product();
            product.MSRP = 1;
            product.SalePrice = .9;
            product.PreviousPriceChangeOccurredAt = DateTime.Now.Subtract(TimeSpan.FromDays(30));

            RedPencil promotion = new RedPencil();
            Assert.AreEqual(true, promotion.IsEligible(product));
        }

        [TestMethod]
        public void PromotionDoesNotTakeEffectWithin30DaysOfPreviousPriceChange()
        {
            Product product = new Product();
            product.MSRP = 1;
            product.SalePrice = .9;
            product.PreviousPriceChangeOccurredAt = DateTime.Now;

            RedPencil promotion = new RedPencil();
            Assert.AreEqual(false, promotion.IsEligible(product));
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void PromotionCanNotLastMoreThan30Days()
        {
            RedPencil promotion = new RedPencil();
            promotion.StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(31));
            promotion.EndDate = DateTime.Now;

            promotion.Begin(new Product());
        }
    }
}
