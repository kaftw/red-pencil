using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPencil
{
    public class RedPencilFactory
    {
        private readonly Dictionary<Product, List<RedPencil>> promotionsDictionary = new Dictionary<Product, List<RedPencil>>();

        public RedPencil CreatePromotion(Product product, DateTime startDate, DateTime endDate)
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

            if (promotions.Any(p => p.Intersects(startDate, endDate)))
                throw new InvalidOperationException();

            var promotion = new RedPencil();
            promotion.StartDate = startDate;
            promotion.EndDate = endDate;
            promotions.Add(promotion);
            return promotion;
        }
    }
}
