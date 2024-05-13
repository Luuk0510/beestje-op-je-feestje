using BeestjeOpJeFeestje.Domain;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class BookingStatusModel
    {
        public string BookingDate { get; set; }

        public List<Animal>? Animals { get; set; }

        public List<Accessories>? Accessories { get; set; }

        public User? User { get; set; }
    }
}
