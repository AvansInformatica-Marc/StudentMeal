using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMeal.AppLogic;
using StudentMeal.DataAccess;
using StudentMeal.Domain;
using StudentMeal.Models;

namespace StudentMeal.Controllers {
    public class HomeController : Controller {
        private static readonly Random random = new Random();

        private readonly StudentMealManager _studentMealManager = new StudentMealManager(RepositoryFactory.RuntimeRepository);

        public IActionResult Index() {
            _studentMealManager.AddNewStudent(new Student {
                Name = "Demo" + random.Next(1000),
                Email = "foo.bar@example.com",
                PhoneNumber = "+31 (6) 12345678"
            });
            return View(_studentMealManager.GetAllStudents());
        }
    }
}
