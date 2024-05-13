using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain
{
    public class CustomerCardType
    {
        public string? Type {  get; set; }

        public virtual ICollection<User> Accounts { get; set; } = new List<User>();
    }
}
