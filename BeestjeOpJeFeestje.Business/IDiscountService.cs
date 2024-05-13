using BeestjeOpJeFeestje.ASP.Models;
using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer
{
    public interface IDiscountService
    {
        public List<DiscountInfo> GetDiscounts(List<Animal> animals, User user, DateOnly date);

        public decimal AddDiscount(decimal totalPrice, List<Animal> animals, User user, DateOnly date);

    }
}
