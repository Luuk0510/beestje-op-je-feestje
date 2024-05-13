using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "U moet een datum kiezen.")]
        [BookingDateValidation(ErrorMessage = "De datum moet minstens 1 dag vanaf vandaag zijn.")]
        public DateTime BookingDate { get; set; }
    }

    public class BookingDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateOnly inputDate = DateOnly.FromDateTime((DateTime)value);
            DateOnly tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            if (inputDate < tomorrow)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
