using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentMeal.AppLogic;
using StudentMeal.Domain;
using StudentMeal.Presentation.Models;

namespace StudentMeal.Presentation.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly StudentMealManager _studentMealManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, StudentMealManager studentMealManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentMealManager = studentMealManager;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel authModel) {
            if (ModelState.IsValid && await SignIn(authModel.Email, authModel.Password)) {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email en/of wachtwoord zijn verkeerd");
            return View();
        }

        public async Task<IActionResult> Logout() {
            await SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel authModel) {
            if (ModelState.IsValid && await Register(authModel.Student, authModel.Password)) {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(nameof(authModel.Student.Email), "Email is mogelijk al in gebruik");
            return View();
        }

        private async Task SignOut() {
            await _signInManager.SignOutAsync();
        }

        private async Task<bool> SignIn(string email, string password) {
            IdentityUser user = await _userManager.FindByNameAsync(email);
            if (user != null) {
                await SignOut();
                return (await _signInManager.PasswordSignInAsync(user, password, false, false)).Succeeded;
            }
            return false;
        }

        private async Task<bool> Register(Student student, string password) {
            var user = new IdentityUser(student.Email);
            if ((await _userManager.CreateAsync(user, password)).Succeeded) {
                _studentMealManager.AddStudent(student);
                await SignIn(student.Email, password);
                return true;
            }
            return false;
        }
    }
}