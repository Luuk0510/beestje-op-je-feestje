using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer
{
    public interface IBookingRepository
    {
        public List<Booking> GetAllBookings();
        public Booking GetBookingById(int id);
        public Booking CreateBooking(Booking booking, List<int> animalIds, List<int> accessoryIds);
        public bool DeleteBooking(int id);
    }
}
