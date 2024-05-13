using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class ContactInformationViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [StringLength(50, ErrorMessage = "Voornaam mag maximaal 50 tekens bevatten.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Tussenvoegsel mag maximaal 50 tekens bevatten.")]
        public string? MiddelName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [StringLength(50, ErrorMessage = "Achternaam mag maximaal 50 tekens bevatten.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Adres is verplicht.")]
        [StringLength(100, ErrorMessage = "Adres mag maximaal 100 tekens bevatten.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is verplicht.")]
        [StringLength(50, ErrorMessage = "Email mag maximaal 50 tekens bevatten.")]
        public string Email { get; set; }

        public BookingStatusModel? BookingStatus { get; set; }
    }
}
