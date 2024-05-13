using BeestjeOpJeFeestje.ASP.Models;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interfaces;
using Moq;
using System.ComponentModel.DataAnnotations;

[TestFixture]
public class AnimalSelectViewModelTests
{
    [Test]
    public void Validate_NoValidationErrors_ShouldPass()
    {
        // Arrange
        var viewModel = new AnimalSelectViewModel
        {
            SelectedAnimalIds = new List<int> { 1, 2 },
            BookingStatus = new BookingStatusModel
            {
                BookingDate = "2024-01-01" 
            }
        };


        var mockAnimalRepository = new Mock<IAnimalRepository>();
        mockAnimalRepository.Setup(repo => repo.GetAllAnimals())
                            .Returns(new List<Animal>
                            {
                                new Animal { Id = 1, Name = "ValidAnimal1", Type = "ValidType", Price = 50, Image = "valid.png" },
                                new Animal { Id = 2, Name = "ValidAnimal2", Type = "ValidType", Price = 60, Image = "valid2.png" },
                            });

        var mockUserRepository = new Mock<IUserRepository>();


        var validationContext = new ValidationContext(viewModel, null, null);
        validationContext.InitializeServiceProvider(type =>
            type == typeof(IAnimalRepository) ? mockAnimalRepository.Object :
            type == typeof(IUserRepository) ? mockUserRepository.Object :
            null);

        // Act
        var validationResults = viewModel.Validate(validationContext).ToList();

        // Assert
        Assert.IsEmpty(validationResults, "Validation errors were not expected.");
    }



    [Test]
    public void Validate_NomNomNomValidation_ShouldFail()
    {
        // Arrange
        var viewModel = new AnimalSelectViewModel
        {
            SelectedAnimalIds = new List<int> { 1, 2 },
            BookingStatus = new BookingStatusModel
            {
                BookingDate = "2024-01-01"
            }
        };

        
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        mockAnimalRepository.Setup(repo => repo.GetAllAnimals())
                            .Returns(new List<Animal>
                            {
                                new Animal { Id = 1, Name = "Leeuw", Type = "Type1", Price = 50, Image = "leeuw.png" },
                                new Animal { Id = 2, Name = "BoerderijAnimal", Type = "Boerderij", Price = 60, Image = "boerderijdier.png" },
                            });

        var mockUserRepository = new Mock<IUserRepository>();
        

        var validationContext = new ValidationContext(viewModel, null, null);
        validationContext.InitializeServiceProvider(type =>
            type == typeof(IAnimalRepository) ? mockAnimalRepository.Object :
            type == typeof(IUserRepository) ? mockUserRepository.Object :
            null);

        // Act
        var validationResults = viewModel.Validate(validationContext).ToList();

        // Assert
        Assert.IsTrue(validationResults.Any(vr => vr.ErrorMessage.Contains("Nom nom nom")), "Expected 'Nom nom nom' validation error.");
    }



}
