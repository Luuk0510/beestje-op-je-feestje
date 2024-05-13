using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer.DiscountRules
{
    public class WeekdayDiscountRule : IDiscountRule
    {
        // booking on monday or Tuesday 15% discount
        public string Name => "Korting maandag of dinsdag";

        public decimal CalculateDiscount(List<Animal> animals, User user, DateOnly date)
        {
            // Check if the booking meets the criteria
            if (IsApplicable(animals, user, date))
            {
                return 15; // 15% discount
            }

            return 0;
        }

        public bool IsApplicable(List<Animal> animals, User user, DateOnly date)
        {
            return date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Tuesday;
        }
    }
}
