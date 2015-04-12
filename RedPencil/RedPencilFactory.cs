using System;
using System.Collections.Generic;
using System.Linq;

namespace RedPencil
{
    public class RedPencilFactory
    {
        private readonly Dictionary<Product, List<RedPencil>> promotionsDictionary = new Dictionary<Product, List<RedPencil>>();

        public RedPencil CreatePromotion(Product product, DateTime startDate, DateTime endDate)
        {
            if (endDate - startDate > TimeSpan.FromDays(30))
                throw new InvalidOperationException();

            List<RedPencil> promotions = GetPromotionsListForProduct(product);

            if (promotions.Any(p => p.Intersects(startDate, endDate)))
                throw new InvalidOperationException();

            var promotion = InnerCreatePromotion(startDate, endDate);
            promotions.Add(promotion);
            return promotion;
        }

        private static RedPencil InnerCreatePromotion(DateTime startDate, DateTime endDate)
        {
            var promotion = new RedPencil();
            promotion.StartDate = startDate;
            promotion.EndDate = endDate;
            return promotion;
        }

        private List<RedPencil> GetPromotionsListForProduct(Product product)
        {
            List<RedPencil> promotions;

            if (!promotionsDictionary.ContainsKey(product))
            {
                promotions = new List<RedPencil>();
                promotionsDictionary[product] = promotions;
            }
            else
            {
                promotions = promotionsDictionary[product];
            }
            return promotions;
        }
    }
}
