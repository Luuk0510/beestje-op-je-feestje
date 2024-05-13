using BeestjeOpJeFeestje.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class FormAnimalViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        [StringLength(50, ErrorMessage = "Naam mag maximaal 50 tekens bevatten.")]
        public string Name { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Prijs is verplicht.")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Afbeelding is verplicht.")]
        [StringLength(50, ErrorMessage = "Afbeelding mag maximaal 50 tekens bevatten.")]
        public string Image { get; set; }

        public List<SelectListItem>? AnimalTypes { get; set; }
    }
}
