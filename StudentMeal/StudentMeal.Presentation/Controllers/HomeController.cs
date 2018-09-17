﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMeal.AppLogic;
using StudentMeal.DataAccess;
using StudentMeal.Models;

namespace StudentMeal.Controllers {
    public class HomeController : Controller {
        private readonly StudentMealManager _studentMealManager = new StudentMealManager(RepositoryFactory.RuntimeRepository);

        public IActionResult Index() {
            return View();
        }
    }
}
