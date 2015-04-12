using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedPencil.Test
{
    [TestClass]
    public class RedPencilFactoryTests
    {
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void PromotionCanNotLastMoreThan30Days()
        {
            var product = CreateTestProduct();
            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now.Subtract(TimeSpan.FromDays(31)), DateTime.Now);

            promotion.Begin(product);
        }

        [TestMethod]
        public void PromotionCanLastUpTo30Days()
        {
            var product = CreateTestProduct();
            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now, DateTime.Now.AddDays(30));
            promotion.Begin(product);
        }

        [TestMethod]
        public void PriceReductionDuringPromotionDoesNotProlongIt()
        {
            var product = CreateTestProduct();

            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now, DateTime.Now.AddDays(30));
            var promotionLength = promotion.EndDate - promotion.StartDate;
            promotion.Begin(product);
            product.SalePrice = .8;

            Assert.AreEqual(promotionLength, promotion.EndDate - promotion.StartDate);
        }

        [TestMethod]
        public void PriceIncreaseDuringPromotionEndsIt()
        {
            var product = CreateTestProduct();

            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now, DateTime.Now.AddDays(30));
            var promotionEnd = promotion.EndDate;
            promotion.Begin(product);
            product.SalePrice = .95;

            Assert.AreNotEqual(promotionEnd, promotion.EndDate);
        }

        [TestMethod]
        public void PriceReductionToTotalDiscountGreaterThan30PercentEndsPromotion()
        {
            var product = CreateTestProduct();

            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now, DateTime.Now.AddDays(30));
            var promotionEnd = promotion.EndDate;
            promotion.Begin(product);
            product.SalePrice = .6;

            Assert.AreNotEqual(promotionEnd, promotion.EndDate);
        }

        [TestMethod]
        public void AnotherPromotionMayBeStartedIfTheyDoNotOverlap()
        {
            var product = CreateTestProduct();
            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now);
            promotion.Begin(product);

            var anotherPromotion = factory.CreatePromotion(product, DateTime.Now.AddMilliseconds(1), DateTime.Now.AddDays(1));
            anotherPromotion.Begin(product);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void AnotherPromotionMayNotBeStartedIfTheyDoOverlap()
        {
            var product = CreateTestProduct();
            var factory = new RedPencilFactory();
            var promotion = factory.CreatePromotion(product, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now);
            promotion.Begin(product);

            var anotherPromotion = factory.CreatePromotion(product, DateTime.Now.Subtract(TimeSpan.FromHours(12)), DateTime.Now.AddDays(1));
            anotherPromotion.Begin(product);
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
