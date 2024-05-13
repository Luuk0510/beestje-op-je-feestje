using System;
using System.Collections.Generic;


namespace BeestjeOpJeFeestje.Domain
{
    public partial class User
    {
     
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddelName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string? CustomerCard { get; set; }

        public virtual CustomerCardType CustomerCardType { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
