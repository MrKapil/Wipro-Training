using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View(new User());

        [HttpPost]
        public IActionResult Login(User input)
        {
            if (!ModelState.IsValid) return View(input);

            // very basic demo auth: any username/password works if password == "1234"
            if (input.Password != "1234")
            {
                ModelState.AddModelError("", "Invalid credentials. Try password: 1234");
                return View(input);
            }

            HttpContext.Session.SetString("username", input.UserName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
