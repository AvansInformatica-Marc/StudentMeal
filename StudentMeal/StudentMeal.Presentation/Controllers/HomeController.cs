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
            var viewModel = new IndexViewModel {
                Today = _studentMealManager.GetMealsForDate(DateTime.Today),
                Upcoming = _studentMealManager.GetMealsForPeriod(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2 * 7))
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult MealInfo(int id) {
            return View(_studentMealManager.GetMealById(id));
        }

        [Authorize, HttpGet]
        public IActionResult NewMeal() {
            return View("EditMeal");
        }

        [Authorize, HttpPost]
        public IActionResult NewMeal(Meal meal) {
            if (_studentMealManager.GetMealsForDate(meal.DateTime).Count() != 0) {
                ModelState.AddModelError(nameof(meal.DateTime), "Er is al een maaltijd op de gegeven datum!");
            }
            if (meal.DateTime.CompareTo(DateTime.Now.AddDays(2 * 7)) > 0) {
                ModelState.AddModelError(nameof(meal.DateTime), "Datum moet in de komende 2 weken zijn.");
            }

            meal.Cook = _studentMealManager.GetStudentByEmail(HttpContext.User.Identity.Name);

            if (ModelState.IsValid) {
                _studentMealManager.AddMeal(meal);
                return View("MealInfo", meal);
            } else {
                return View("EditMeal");
            }
        }

        [Authorize]
        public IActionResult JoinMeal(int id) {
            var meal = _studentMealManager.GetMealById(id);
            var student = GetLoggedInStudent();
            if (meal.CanStudentEnterAsGuest(student.Email)) {
                _studentMealManager.AddStudentToMealAsGuest(student, meal);
            }

            return View("MealInfo", meal);
        }

        [Authorize]
        public IActionResult LeaveMeal(int id) {
            var meal = _studentMealManager.GetMealById(id);
            var student = GetLoggedInStudent();
            if (meal.CanStudentLeave(student.Email)) {
                _studentMealManager.RemoveStudentFromMealAsGuest(student, meal);
            }

            return View("MealInfo", meal);
        }

        [Authorize]
        public IActionResult DeleteMeal(int id) {
            var meal = _studentMealManager.GetMealById(id);
            var student = GetLoggedInStudent();
            if (meal.CanStudentDelete(student.Email)) {
                _studentMealManager.DeleteMeal(meal);
                return RedirectToAction("Index");
            }

            return View("MealInfo", meal);
        }

        [Authorize]
        public IActionResult EditMeal(int id) {
            var meal = _studentMealManager.GetMealById(id);
            var student = GetLoggedInStudent();
            if (meal.CanStudentEdit(student.Email)) {
                return View(meal);
            }

            return View("MealInfo", meal);
        }

        [Authorize, HttpPost]
        public IActionResult EditMeal(Meal meal) {
            var mealsOnDate = _studentMealManager.GetMealsForDate(meal.DateTime);
            if (mealsOnDate.Count() != 0 && mealsOnDate.FirstOrDefault().Id != meal.Id) {
                ModelState.AddModelError(nameof(meal.DateTime), "Er is al een maaltijd op de gegeven datum!");
            }
            if (meal.DateTime.CompareTo(DateTime.Now.AddDays(2 * 7)) > 0) {
                ModelState.AddModelError(nameof(meal.DateTime), "Datum moet in de komende 2 weken zijn.");
            }

            meal.Cook = _studentMealManager.GetStudentByEmail(HttpContext.User.Identity.Name);

            if (ModelState.IsValid) {
                _studentMealManager.UpdateMeal(meal.Id, oldMeal => meal);
                return View("MealInfo", meal);
            } else {
                return View();
            }
        }

        private Student GetLoggedInStudent() {
            return _studentMealManager.GetStudentByEmail(HttpContext.User.Identity.Name);
        }
    }
}