using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interfaces
{
    public interface IAnimalTypeRepository
    {
        public List<AnimalType> GetAllAnimalTypes();
    }
}
