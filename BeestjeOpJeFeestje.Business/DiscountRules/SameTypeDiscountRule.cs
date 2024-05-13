using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer.DiscountRules
{
    public class SameTypeDiscountRule : IDiscountRule
    {
        // 10% discount for 3 animals of the same type
        public string Name => "Korting type";

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
            // Check if there are exactly 3 animals in the booking
            if (animals != null && animals.Count() >= 3)
            {
                // Check if all animals have the same type
                string firstAnimalType = animals.First().Type;
                if (animals.All(animal => animal.Type == firstAnimalType))
                {
                    return true; // Applicable if all animals are of the same type
                }
            }

            return false;
        }
    }

}
