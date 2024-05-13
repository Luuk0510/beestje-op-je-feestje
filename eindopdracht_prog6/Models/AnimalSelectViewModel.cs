using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class AnimalSelectViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Je moet een animal kiezen om verder te gaan!")]
        public List<int> SelectedAnimalIds { get; set; }
        public List<Animal>? AvailableAnimals { get; set; }
        public BookingStatusModel? BookingStatus { get; set; }
        public string? LoggedInUserEmail { get; set; }
        public DateTime BookingDate { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) 
        {
            var _userRepository = validationContext.GetService(typeof(IUserRepository)) as IUserRepository;
            var _animalRepository = validationContext.GetService(typeof(IAnimalRepository)) as IAnimalRepository;
            var animals = _animalRepository.GetAllAnimals();
           
            if (SelectedAnimalIds != null)
            {
                var LoggedInUser = _userRepository.GetUserByMail(LoggedInUserEmail);
                var selectedAnimals = animals.Where(animal => SelectedAnimalIds.Contains(animal.Id));
                

                //check for 'Nom nom nom'
                if (selectedAnimals.Any(animal => animal.Name == "Leeuw" || animal.Name == "IJsbeer"))
                {
                    if (selectedAnimals.Any(animal => animal.Type == "Boerderij"))
                    {
                        yield return new ValidationResult("Nom nom nom", new[] { nameof(SelectedAnimalIds) });
                    }
                }

                //check for 'Dieren in pak werken alleen doordeweeks!'
                if (selectedAnimals.Any(animal => animal.Name == "Pinguïn"))
                {
                    

                    if (BookingDate.DayOfWeek == DayOfWeek.Saturday || BookingDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        yield return new ValidationResult("Dieren in pak werken alleen doordeweeks!", new[] { nameof(SelectedAnimalIds) });
                    }
                }

                // Check for 'Brrrr – Veelste koud'
                if (selectedAnimals.Any(animal => animal.Type == "Woestijn"))
                {

                    if (BookingDate.Month >= 10 || BookingDate.Month <= 2)
                    {
                        yield return new ValidationResult("Brrrr – Veelste koud", new[] { nameof(SelectedAnimalIds) });
                    }
                }

                // Check for 'Some People Are Worth Melting For. ~ Olaf'
                if (selectedAnimals.Any(animal => animal.Type == "Sneeuw"))
                {

                    if (BookingDate.Month >= 6 && BookingDate.Month <= 8)
                    {
                        yield return new ValidationResult("Some People Are Worth Melting For. ~ Olaf", new[] { nameof(SelectedAnimalIds) });
                    }
                }

               if (LoggedInUser != null)
                {
                    if (!LoggedInUser.CustomerCard.Any())
                    {
                        if (selectedAnimals.Count() > 3) //max 3 animal
                        {
                            yield return new ValidationResult("Je mag maar 3 dieren boeken", new[] { nameof(SelectedAnimalIds) });
                        }
                    }
                    else if (LoggedInUser.CustomerCard == "Zilver")
                    {
                        if (selectedAnimals.Count() > 4) //max 4 animal
                        {
                            yield return new ValidationResult("Je mag maar 3 dieren boeken", new[] { nameof(SelectedAnimalIds) });
                        }
                    }
                    if (LoggedInUser.CustomerCard != "Platina")
                    {
                        if (selectedAnimals.Any(animal => animal.Type == "VIP"))
                        {
                            yield return new ValidationResult("Je mag geen VIP dieren boeken", new[] { nameof(SelectedAnimalIds) });
                        }
                    }
                } else
                {
                    if (selectedAnimals.Any(animal => animal.Type == "VIP"))
                    {
                        yield return new ValidationResult("Je mag geen VIP dieren boeken", new[] { nameof(SelectedAnimalIds) });
                    }
                    if (selectedAnimals.Count() > 3) //max 3 animal
                    {
                        yield return new ValidationResult("Je mag maar 3 dieren boeken", new[] { nameof(SelectedAnimalIds) });
                    }
                }
                
                
            }
        }
    }
}
