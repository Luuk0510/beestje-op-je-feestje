using BeestjeOpJeFeestje.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Sql
{
    public class AnimalRepositorySql : IAnimalRepository
    {
        readonly BeestjeOpJeFeestjeContext _context;

        public AnimalRepositorySql(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public Animal CreateAnimal(Animal beestje)
        {
            _context.Animals.Add(beestje);
            _context.SaveChanges();
            return beestje;
        }

        public bool DeleteAnimal(int id)
        {
            var toRemove = _context.Animals.Find(id);
            if (toRemove != null)
            {
                _context.Animals.Remove(toRemove);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Animal> GetAllAnimals() => _context.Animals.Include(t => t.AnimalType).Include(b => b.Bookings).ToList();


        public Animal GetAnimalById(int id) => _context.Animals.Include(t => t.AnimalType).Include(b => b.Bookings).FirstOrDefault(m => m.Id == id);


        public Animal UpdateAnimal(Animal Animal)
        {
            _context.Attach(Animal);
            _context.Animals.Update(Animal);
            _context.SaveChanges();
            return Animal;
        }
    }
}
