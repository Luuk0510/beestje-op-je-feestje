using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer.DiscountRules
{
    public interface IDiscountRule
    {
        string Name { get; }
        decimal CalculateDiscount(List<Animal> animals, User user, DateOnly dateg);
        bool IsApplicable(List<Animal> animals, User user, DateOnly date);
    }
}
