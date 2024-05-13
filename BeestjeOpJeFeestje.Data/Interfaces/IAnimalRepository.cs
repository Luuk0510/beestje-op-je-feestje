using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interfaces
{
    public interface IAnimalRepository
    {
        public List<Animal> GetAllAnimals();
        public Animal GetAnimalById(int id);
        public Animal CreateAnimal(Animal beestje);
        public Animal UpdateAnimal(Animal beestje);
        public bool DeleteAnimal(int id);
    }
}
