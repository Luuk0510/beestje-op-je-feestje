using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer.DiscountRules
{
    public class LetterDiscountRule : IDiscountRule
    {
        // A booking with an animal in its name containing the letter 'A' gets an additional 2% discount.
        // If there is also the letter B, an extra 2% discount is applied;
        // If there is then also the letter C, an additional 2% is added;
        // And so on.
        public string Name => "Korting letters";


        public decimal CalculateDiscount(List<Animal> animals, User user, DateOnly date)
        {
            // Check if the booking meets the criteria
            if (IsApplicable(animals, user, date))
            {
                int extraPercentage = CalculateExtraPercentage(animals);

                // Apply extra percentage discount
                return extraPercentage;
            }

            return 0; // No discount if not applicable
        }

        public bool IsApplicable(List<Animal> animals, User user, DateOnly date)
        {
            // Check if there is an animal with a name containing the letter 'A'
            return animals != null && animals.Any(animal => animal.Name.Contains("A"));
        }

        private int CalculateExtraPercentage(List<Animal> animals)
        {
            int extraPercentage = 0;

            // Define the letters for which extra percentage is granted
            string lettersForExtraDiscount = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (char letter in lettersForExtraDiscount)
            {
                if (animals.Any(animal => animal.Name.Contains(letter)))
                {
                    extraPercentage += 2;
                } else
                {
                    break;
                }
            }

            return extraPercentage;
        }
    }
}
