using BeestjeOpJeFeestje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Sql
{
    public class CustomerCardTypeRepositorySql : ICustomerCardTypeRepository
    {
        readonly BeestjeOpJeFeestjeContext _context;

        public CustomerCardTypeRepositorySql(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public List<CustomerCardType> GetAllCustomerCardType() => _context.CustomerCardTypes.ToList();

    }
}
