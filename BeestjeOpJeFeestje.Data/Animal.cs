using System;
using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Domain
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual AnimalType AnimalType { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
