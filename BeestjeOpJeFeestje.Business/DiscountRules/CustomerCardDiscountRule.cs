using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer.DiscountRules
{
    public class CustomerCardDiscountRule : IDiscountRule
    {
        //Discount for users with a customer card is 10%
        public string Name => "Korting klantenkaart";

        public decimal CalculateDiscount(List<Animal> animals, User user, DateOnly date)
        {
            // Check if the booking meets the criteria
            if (IsApplicable(animals, user, date))
            {
                return 10; // 10% discount
            }

            return 0; // No discount if not applicable
        }

        public bool IsApplicable(List<Animal> animals, User user, DateOnly date)
        {
            // Check if the customer has a customer card
            return user != null && !string.IsNullOrEmpty(user.CustomerCard);
        }
    }
}
