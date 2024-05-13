using BeestjeOpJeFeestje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Sql
{
    public class AccessoriesRepositorySql : IAccessoriesRepository
    {
        readonly BeestjeOpJeFeestjeContext _context;

        public AccessoriesRepositorySql(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public List<Accessories> GetAllAccessories() => _context.Accessories.ToList();

        public Accessories GetAccessoryById(int id) => _context.Accessories
            .FirstOrDefault(a => a.Id == id);
    }
}
