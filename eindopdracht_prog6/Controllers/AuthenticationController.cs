using BeestjeOpJeFeestje.ASP.Models;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ASP.Controllers
{
    public class AuthenticationController : Controller
    {   
        private string _returnUrl { get; set; }
        private const int PASSWORDLENGHT = 12;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerCardTypeRepository _customerCardTypeRepo;

        public AuthenticationController(IUserRepository userRepository, ICustomerCardTypeRepository customerCardTypeRepo, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _customerCardTypeRepo = customerCardTypeRepo;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login(bool booking = false)
        {
            var model = new LoginViewModel();
            model.Booking = booking;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = _userRepository.GetUserByMail(model.Email);

                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    if (user.MiddelName != null)
                    {
                        HttpContext.Session.SetString("MiddelName", user.MiddelName);

                    }
                    HttpContext.Session.SetString("Surname", user.Surname);
                    HttpContext.Session.SetString("Address", user.Address);
                    HttpContext.Session.SetString("Email", user.Email);

                    if (model.Booking == true)
                    {
                        return RedirectToAction("Bevestiging", "Boeking");
                    }
                    return RedirectToAction("Index", "Boeking");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ongeldige gebruikersnaam of wachtwoord");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            ISession session = HttpContext.Session;
            var sessionKeys = session.Keys;

            foreach (var key in sessionKeys)
            {
                session.Remove(key);
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Boeking");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Register()
        {
            List<IdentityRole> GetAlleRoles = _userRepository.GetAlleRoles();
            List<CustomerCardType> customerCardTypes = _customerCardTypeRepo.GetAllCustomerCardType();

            RegisterFormViewModel viewModel = new RegisterFormViewModel
            {
                UserRoles = GetAlleRoles,
                CustomerCardTypes = customerCardTypes
            };

            return View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register(RegisterFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string generatedPassword = PasswordGenerator.GenerateRandomPassword(PASSWORDLENGHT); 

                var aspUser = new IdentityUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await _userManager.CreateAsync(aspUser, generatedPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(aspUser, viewModel.SelectedUserRole);

                    var user = new User
                    {
                        FirstName = viewModel.FirstName,
                        MiddelName = viewModel.MiddelName,
                        Surname = viewModel.Surname,
                        Address = viewModel.Address,
                        Email = viewModel.Email,
                        PhoneNumber= int.Parse(viewModel.PhoneNumber),
                        CustomerCard = viewModel.SelectedCustomerCardType
                    };

                    var model = _userRepository.CreateUser(user);



                    return RedirectToAction("RegisterSuccess", new { email = viewModel.Email, password = generatedPassword });

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            viewModel.UserRoles = _userRepository.GetAlleRoles();
            viewModel.CustomerCardTypes = _customerCardTypeRepo.GetAllCustomerCardType();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult RegisterSuccess(string email, string password)
        {
            var viewModel = new LoginViewModel
            {
                Email = email,
                Password = password
            };

            return View(viewModel);
        }

    }
}
