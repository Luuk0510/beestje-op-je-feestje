using BeestjeOpJeFeestje.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Sql
{
    public class AnimalTypeRepositorySql : IAnimalTypeRepository
    {
        readonly BeestjeOpJeFeestjeContext _context;
        public AnimalTypeRepositorySql(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public List<AnimalType> GetAllAnimalTypes() => _context.AnimalTypes.ToList();

    }
}
