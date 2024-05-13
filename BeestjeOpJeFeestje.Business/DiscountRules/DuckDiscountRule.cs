using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer.DiscountRules
{
    public class DuckDiscountRule : IDiscountRule
    {
        //Booking of a duck with a chance of 1 to 6 for 50% Discount.
        public string Name => "Korting Eend";

        public decimal CalculateDiscount(List<Animal> animals, User user, DateOnly date)
        {
            // Check if the booking meets the criteria
            if (IsApplicable(animals, user, date))
            {
                // Generate a random number between 1 and 6
                Random random = new Random();
                int randomNumber = random.Next(1, 7);

                // Apply 50% discount if the random number is 1
                if (randomNumber == 1)
                {
                    return 50; // 50% discount
                }
            }

            return 0;
        }

        public bool IsApplicable(List<Animal> animals, User user, DateOnly date)
        {
            // Check if there is an animal with the name 'Eend' in the booking
            if (animals != null && animals.Any(animal => animal.Name == "Eend"))
            {
                return true; // Applicable if there is an animal with the name 'Eend'
            }

            return false;
        }
    }
}
