using System;
using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Domain
{
    public partial class AnimalType
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
