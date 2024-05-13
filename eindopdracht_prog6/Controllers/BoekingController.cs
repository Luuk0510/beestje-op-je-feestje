using BeestjeOpJeFeestje.ASP.Models;
using BeestjeOpJeFeestje.BusinessLayer;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using System;

namespace BeestjeOpJeFeestje.ASP.Controllers
{
    public class BoekingController : Controller
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IAnimalRepository _animalRepo;
        private readonly IAccessoriesRepository _accessoriesRepo;
        private readonly IDiscountService _discountService;
        private readonly IUserRepository _userRepo;

        public BoekingController(IBookingRepository bookingRepository, IAnimalRepository animalRespository, IDiscountService discountService, IUserRepository userRepository, IAccessoriesRepository accessoriesRepo)
        {
            _bookingRepo = bookingRepository;
            _animalRepo = animalRespository;
            _discountService = discountService;
            _userRepo = userRepository;
            _accessoriesRepo = accessoriesRepo;
        }

        public IActionResult Index()
        {
            ISession session = HttpContext.Session;
            var sessionKeys = session.Keys;
// List of session keys to preserve
var keysToPreserve = new List<string> { "FirstName", "MiddelName", "Surname", "Address", "Email" };

foreach (var key in sessionKeys)
{
    if (!keysToPreserve.Contains(key))
    {
        session.Remove(key);
    }
}


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string bookingDate = viewModel.BookingDate.ToShortDateString();
                HttpContext.Session.SetString("BookingDate", bookingDate.ToString());

                return RedirectToAction("Beestje");
            }

            return View(viewModel);
        }

        public IActionResult Beestje()
        {
            var bookingStatus = GetBookingStatus();

            var bookingen = _bookingRepo.GetAllBookings();
            var animals = _animalRepo.GetAllAnimals();

            //animals witouth booking on date
            var availableAnimals = animals.Where(a => !bookingen.Any(b => b.Date == DateTime.Parse(bookingStatus.BookingDate) && b.Animals.Any(a => a.Id == b.Id))).ToList();

            string userEmail = null;

            if (bookingStatus.User.Email != null)
            {
                userEmail = bookingStatus.User.Email;
            }

            var viewModel = new AnimalSelectViewModel
            {
                AvailableAnimals = availableAnimals,
                LoggedInUserEmail = userEmail,
                BookingStatus = bookingStatus
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Beestje(AnimalSelectViewModel viewModel)
        {
            //check if its valid.
            if (!ModelState.IsValid)
            {
                var bookingStatus = GetBookingStatus();
                string userEmail = null;

                if (bookingStatus.User.Email != null)
                {
                    userEmail = bookingStatus.User.Email;
                }

                var bookingen = _bookingRepo.GetAllBookings();
                var animals = _animalRepo.GetAllAnimals();

                //animals witouth booking on date
                var availableAnimals = animals.Where(a => !bookingen.Any(b => b.Date == DateTime.Parse(bookingStatus.BookingDate) && b.Animals.Any(a => a.Id == b.Id))).ToList();

                viewModel.AvailableAnimals = availableAnimals;
                viewModel.LoggedInUserEmail = userEmail;
                viewModel.BookingStatus = bookingStatus;

                return View(viewModel);
            }

            var animalsIds = viewModel.SelectedAnimalIds;

            string serializedAnimalIds = JsonSerializer.Serialize(animalsIds, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            HttpContext.Session.SetString("SelectedAnimalIds", serializedAnimalIds);

            return RedirectToAction("Accessoires");
        }

        public IActionResult Accessoires()
        {
            var bookingStatus = GetBookingStatus();

            var allAccessoires = _accessoriesRepo.GetAllAccessories();

            var viewModel = new AccessoiresSelectViewModel
            {
                AvailableAccessoires = allAccessoires,
                BookingStatus = bookingStatus,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accessoires(AccessoiresSelectViewModel viewModel)
        {
            var accessoiresIds = viewModel.SelectedAccessoiresIds;

            string serializedAccessyIds = JsonSerializer.Serialize(accessoiresIds, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            HttpContext.Session.SetString("SelectedAccessoryIds", serializedAccessyIds);

            if (User.FindFirstValue(ClaimTypes.Email) != null)
            {
                return RedirectToAction("Bevestiging");
            }

            return RedirectToAction("Gegevens");
        }

        public IActionResult Gegevens()
        {
            var bookingStatus = GetBookingStatus();

            ContactInformationViewModel viewModel = new ContactInformationViewModel
            {
                BookingStatus = bookingStatus
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Gegevens(ContactInformationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string email = HttpContext.Session.GetString("Email");

                if (email == null)
                {
                    HttpContext.Session.SetString("FirstName", viewModel.FirstName);
                    if (viewModel.MiddelName != null)
                    {
                        HttpContext.Session.SetString("MiddelName", viewModel.MiddelName);
                    }
                    HttpContext.Session.SetString("Surname", viewModel.Surname);
                    HttpContext.Session.SetString("Address", viewModel.Address);
                    HttpContext.Session.SetString("Email", viewModel.Email);
                }

                return RedirectToAction("Bevestiging");
            }

            var bookingStatus = GetBookingStatus();
            viewModel.BookingStatus = bookingStatus;

            return View(viewModel);
        }

        public IActionResult Bevestiging()
        {
            var bookingStatus = GetBookingStatus();

            var discounts = _discountService.GetDiscounts(bookingStatus.Animals, bookingStatus.User, DateOnly.Parse(bookingStatus.BookingDate));

            decimal totalPrice = 0;

            foreach (var animal in bookingStatus.Animals)
            {
                totalPrice += animal.Price;
            }

            foreach (var accessory in bookingStatus.Accessories)
            {
                totalPrice += accessory.Price;
            }

            decimal totalPriceWithDiscount = _discountService.AddDiscount(totalPrice, bookingStatus.Animals, bookingStatus.User, DateOnly.Parse(bookingStatus.BookingDate));

            BevestigingViewModel viewModel = new BevestigingViewModel
            {
                TotalPrice = totalPriceWithDiscount,
                Discounts = discounts,
                BookingStatus = bookingStatus
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Bevestiging(BevestigingViewModel viewModel)
        {
            var bookingStatus = GetBookingStatus();
            List<int> animalIds = bookingStatus.Animals?.Select(a => a.Id).ToList();
            List<int> accessoryIds = bookingStatus.Accessories?.Select(a => a.Id).ToList();

            _userRepo.CreateUser(bookingStatus.User);

            var userId = _userRepo.GetUserByMail(bookingStatus.User.Email).Id;

            var booking = new Booking
            {
                AccountId = userId,
                Date = DateTime.Parse(bookingStatus.BookingDate),
                TotalPrice = viewModel.TotalPrice
            };

            _bookingRepo.CreateBooking(booking, animalIds, accessoryIds);

            ISession session = HttpContext.Session;
            var sessionKeys = session.Keys;

            foreach (var key in sessionKeys)
            {
                session.Remove(key);
            }

            TempData["BookingSucces"] = "Uw boeking was succesvol!";

            return View("Index");
        }

        public async Task<IActionResult> BookingList()
        {
            var bookings = _bookingRepo.GetAllBookings();
            var user = _userRepo.GetUserByMail(User.FindFirstValue(ClaimTypes.Email));

            var userBookings = bookings.Where(u => u.Users == user).ToList();

            return View(userBookings);
        }


        


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = _bookingRepo.GetBookingById(id.Value);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success =  _bookingRepo.DeleteBooking(id);

            if (success)
            {
                TempData["SuccessMessage"] = "Boeking is succesvol verwijdert.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error bij het verwijderen van de boeking, probeer later opnieuw.";
            }
            return RedirectToAction(nameof(BookingList));
        }

        private BookingStatusModel GetBookingStatus()
        {
            string bookingDate = HttpContext.Session.GetString("BookingDate");
            string serializedAnimalIds = HttpContext.Session.GetString("SelectedAnimalIds");
            string serializedAccessoryIds = HttpContext.Session.GetString("SelectedAccessoryIds");
            string firstName = HttpContext.Session.GetString("FirstName");
            string middlename = HttpContext.Session.GetString("MiddelName");
            string surname = HttpContext.Session.GetString("Surname");
            string address = HttpContext.Session.GetString("Address");
            string email = HttpContext.Session.GetString("Email");

            List<Animal> selectedAnimals = new List<Animal>();
            List<Accessories> selectedAccessoires = new List<Accessories>();
            User user = new User();

            if (!string.IsNullOrEmpty(serializedAnimalIds))
            {
                var selectedAnimalIds = JsonSerializer.Deserialize<List<int>>(serializedAnimalIds, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

                foreach (var animalid in selectedAnimalIds)
                {
                    Animal animal = _animalRepo.GetAnimalById(animalid);
                    selectedAnimals.Add(animal);
                }
            }

            if (!string.IsNullOrEmpty(serializedAccessoryIds) && serializedAccessoryIds != "null")
            {
                var selectedAccessoryIds = JsonSerializer.Deserialize<List<int>>(serializedAccessoryIds, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

                foreach (var accessoryId in selectedAccessoryIds)
                {
                    Accessories accossoires = _accessoriesRepo.GetAccessoryById(accessoryId);
                    selectedAccessoires.Add(accossoires);
                }
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                user.FirstName = firstName;
                user.MiddelName = middlename;
                user.Surname = surname;
                user.Address = address;
                user.Email = email;
            }

            BookingStatusModel bookingStatus = new BookingStatusModel
            {
                BookingDate = bookingDate,
                Animals = selectedAnimals,
                Accessories = selectedAccessoires,
                User = user
            };

            return bookingStatus;
        }
    }
}
