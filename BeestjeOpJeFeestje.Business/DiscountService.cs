using BeestjeOpJeFeestje.ASP.Models;
using BeestjeOpJeFeestje.BusinessLayer.DiscountRules;
using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer
{
    public class DiscountService : IDiscountService
    {
        private readonly List<IDiscountRule> _discountRules;

        public DiscountService(List<IDiscountRule> discountRules)
        {
            _discountRules = discountRules;
        }

        public List<DiscountInfo> GetDiscounts(List<Animal> animals, User user, DateOnly date)
        {
            var discounts = new List<DiscountInfo>();

            foreach (var discountRule in _discountRules)
            {
                decimal discountAmount = discountRule.CalculateDiscount(animals, user, date);

                if (discountAmount > 0)
                {
                    discounts.Add(new DiscountInfo
                    {
                        Name = discountRule.Name,
                        Discounts = discountAmount
                    });
                }
            }

            return discounts;
        }

        public decimal AddDiscount(decimal totalPrice, List<Animal> animals, User user, DateOnly date)
        {
            decimal totalDiscount = 0;
     

            foreach (var discountRule in _discountRules)
            {
                totalDiscount += discountRule.CalculateDiscount(animals, user, date);
            }

            if (totalDiscount > 60)
            {
                totalDiscount = 60;
            }


            decimal newPrice = totalPrice / 100 * (100 -totalDiscount);
            
            // Pas de totale korting toe op het totale bedrag
             

            
            return newPrice;
        }

    }
}
