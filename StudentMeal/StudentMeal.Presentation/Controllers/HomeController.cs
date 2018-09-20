using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMeal.AppLogic;
using StudentMeal.DataAccess;
using StudentMeal.Domain;
using StudentMeal.Presentation.Models;

namespace StudentMeal.Controllers {
    public class HomeController : Controller {
        private readonly IServiceProvider _serviceProvider;

        public HomeController([FromServices] IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        private StudentMealManager CreateStudentMealManager() {
            return new StudentMealManager(new RepositoryFactory(_serviceProvider).FakeDataRepository);
        }

        public IActionResult Index() {
            using (var manager = CreateStudentMealManager()) {
                return View(new IndexViewModel {
                    Today = manager.GetMealsForDate(DateTime.Today),
                    Upcoming = manager.GetMealsForPeriod(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2 * 7))
                });
            }
        }

        public IActionResult MealInfo(int id) {
            using (var manager = CreateStudentMealManager()) {
                return View(manager.GetMealById(id));
            }
        }

        [HttpGet]
        public IActionResult NewMeal() {
            return View();
        }

        [HttpPost]
        public IActionResult NewMeal(Meal meal) {
            meal.Cook = new Student {
                Id = 3,
                Name = "Elon",
                Email = "elon.musk@student.avans.nl",
                PhoneNumber = "+31 (6) 87654321"
            };
            if (ModelState.IsValid) {
                using (var manager = CreateStudentMealManager()) {
                    manager.AddMeal(meal);
                }
                return View("MealInfo", meal);
            } else {
                return View();
            }
        }
    }
}