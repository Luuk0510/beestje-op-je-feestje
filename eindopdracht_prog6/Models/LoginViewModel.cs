using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is verplicht.")]
        [StringLength(50, ErrorMessage = "Email mag maximaal 50 tekens bevatten.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [StringLength(50, ErrorMessage = "Wachtwoord mag maximaal 50 tekens bevatten.")]
        public string Password { get; set; }
        
        public bool Booking { get; set; }
    }
}
