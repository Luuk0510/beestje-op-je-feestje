using System;
using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Domain
{
    public partial class Booking
    {
        public Booking()
        {
            Animals = new List<Animal>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual User Users { get; set; } = null!;
        public virtual ICollection<Animal> Animals { get; set; }
        public virtual ICollection<Accessories> Accessories { get; set; }
    }
}
