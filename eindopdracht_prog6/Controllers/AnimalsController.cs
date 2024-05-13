using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interfaces;
using BeestjeOpJeFeestje.ASP.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeestjeOpJeFeestje.ASP.Controllers
{
    
    public class AnimalsController : Controller
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IAnimalTypeRepository _animalTypeRepo;

        public AnimalsController(IAnimalRepository animalRepository, IAnimalTypeRepository animalTypeRepository)
        {
            _animalRepo = animalRepository;
            _animalTypeRepo = animalTypeRepository;
        }


        public async Task<IActionResult> Index()
        {
            var animals = _animalRepo.GetAllAnimals();
            return View(animals);
        }


        public async Task<IActionResult> Details(int? id)
        {
            var animal = _animalRepo.GetAnimalById(id.Value);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        public IActionResult Create()
        {
            var animalTypes = _animalTypeRepo.GetAllAnimalTypes()
            .Select(at => new SelectListItem { Text = at.Name, Value = at.Name })
            .ToList();

            var viewModel = new FormAnimalViewModel
            {
                AnimalTypes = animalTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormAnimalViewModel formAnimalViewModel)
        {
            if (!ModelState.IsValid)
            {

                var animalTypes = _animalTypeRepo.GetAllAnimalTypes()
                .Select(at => new SelectListItem { Text = at.Name, Value = at.Name })
                .ToList();

                formAnimalViewModel.AnimalTypes = animalTypes;

                return View(formAnimalViewModel);
            }

            var animal = new Animal
            {
                Name = formAnimalViewModel.Name,
                Type = formAnimalViewModel.Type,
                Price = formAnimalViewModel.Price,
                Image = formAnimalViewModel.Image
            };

            var model = _animalRepo.CreateAnimal(animal);
            TempData["SuccessMessage"] = "Animal has been created successfully.";
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _animalRepo.GetAnimalById(id.Value);

            if (animal == null)
            {
                return NotFound();
            }

            var animalTypes = _animalTypeRepo.GetAllAnimalTypes()
        .   Select(at => new SelectListItem { Text = at.Name, Value = at.Name })
             .ToList();

            var viewModel = new FormAnimalViewModel
            {
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                Image = animal.Image,
                AnimalTypes = animalTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormAnimalViewModel formAnimalViewModel)
        {
            if (id != formAnimalViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var animalTypes = _animalTypeRepo.GetAllAnimalTypes()
                    .Select(at => new SelectListItem { Text = at.Name, Value = at.Name })
                    .ToList();

                formAnimalViewModel.AnimalTypes = animalTypes;

                return View(formAnimalViewModel);
            }

            var animal = new Animal
            {
                Id = id,
                Name = formAnimalViewModel.Name,
                Type = formAnimalViewModel.Type,
                Price = formAnimalViewModel.Price,
                Image = formAnimalViewModel.Image
            };

            _animalRepo.UpdateAnimal(animal);

            TempData["SuccessMessage"] = "Animal is succesvol aangepast.";
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _animalRepo.GetAnimalById(id.Value);

            if (animal == null)
            {
                return NotFound();
            }

            if (AnimalHasBookings(animal))
            {
                TempData["ErrorMessage"] = "Dit dier kan niet worden verwijderd omdat er boekingen voor zijn.";
                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = _animalRepo.DeleteAnimal(id);

            if (success)
            {
                TempData["SuccessMessage"] = "Dier is succesvol verwijder.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error bij het verwijderen van het dier. Probeer het later opnieuw";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalHasBookings(Animal animal)
        {
            return animal.Bookings != null && animal.Bookings.Any();
        }
    }
}
