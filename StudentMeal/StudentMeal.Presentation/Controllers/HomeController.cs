using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMeal.AppLogic;
using StudentMeal.Domain;
using StudentMeal.Presentation.Models;

namespace StudentMeal.Controllers {
    public class HomeController : Controller {
        private readonly StudentMealManager _studentMealManager;

        public HomeController(StudentMealManager studentMealManager) {
            _studentMealManager = studentMealManager;
        }

        public IActionResult Index() {
            return View(new IndexViewModel {
                Today = _studentMealManager.GetMealsForDate(DateTime.Today),
                Upcoming = _studentMealManager.GetMealsForPeriod(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2 * 7))
            });
        }

        [Authorize]
        public IActionResult MealInfo(int id) {
            return View(_studentMealManager.GetMealById(id));
        }

        [Authorize, HttpGet]
        public IActionResult NewMeal() {
            return View();
        }

        [Authorize, HttpPost]
        public IActionResult NewMeal(Meal meal) {
            if (_studentMealManager.GetMealsForDate(meal.DateTime).Count() != 0) {
                ModelState.AddModelError(nameof(meal.DateTime), "Er is al een maaltijd op de gegeven datum!");
            }

            meal.Cook = new Student {
                Id = 3,
                Name = "Elon",
                Email = "elon.musk@student.avans.nl",
                PhoneNumber = "+31 (6) 87654321"
            };

            if (ModelState.IsValid) {
                //_studentMealManager.AddMeal(meal);
                return View("MealInfo", meal);
            } else {
                return View();
            }
        }
    }
}