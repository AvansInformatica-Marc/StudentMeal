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
        //private static readonly Random random = new Random();

        private readonly StudentMealManager _studentMealManager = new StudentMealManager(RepositoryFactory.FakeDataRepository);

        public IActionResult Index() {
            return View(new IndexViewModel {
                Today = _studentMealManager.GetMealsForDate(DateTime.Today),
                Upcoming = _studentMealManager.GetMealsForPeriod(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2 * 7))
            });
        }
    }
}
