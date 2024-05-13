using BeestjeOpJeFeestje.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.BusinessLayer
{
    public class BookingRepositorySql : IBookingRepository
    {
        readonly BeestjeOpJeFeestjeContext _context;

        public BookingRepositorySql(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public Booking CreateBooking(Booking booking, List<int> animalIds, List<int> accessoryIds)
        {
            var animals = _context.Animals.Where(a => animalIds.Contains(a.Id)).ToList();
            foreach (var animal in animals)
            {
                booking.Animals.Add(animal);
            }

            var accessories = _context.Accessories.Where(a => accessoryIds.Contains(a.Id)).ToList();
            if (booking.Accessories == null)
            {
                booking.Accessories = new List<Accessories>();
            }
            foreach (var accessory in accessories)
            {
                booking.Accessories.Add(accessory);
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public bool DeleteBooking(int id)
        {
            var toRemove = _context.Bookings.Find(id);
            if (toRemove != null)
            {
                _context.Bookings.Remove(toRemove);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Booking> GetAllBookings() => _context.Bookings.Include(a => a.Users).Include(b => b.Animals).ToList();

        public Booking GetBookingById(int id) => _context.Bookings.Include(a => a.Users).Include(b => b.Animals).FirstOrDefault(m => m.Id == id);
    }
}
