using Microsoft.AspNetCore.Mvc;
using MvcModelBindingDemo.Models;

namespace MvcModelBindingDemo.Controllers
{
    public class HomeController : Controller
    {
        // Landing page with links
        public IActionResult Index() => View();

        // --- Simple model binding (User with simple properties) ---
        [HttpGet]
        public IActionResult SimpleForm()
        {
            // pass an empty model to view
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleResult(User user)
        {
            // Model binding populates User.FirstName, LastName, Age
            if (!ModelState.IsValid) return View("SimpleForm", user);
            return View("SimpleResult", user);
        }

        // --- Complex model binding (User with nested Address) ---
        [HttpGet]
        public IActionResult ComplexForm()
        {
            // Ensure Address instance is present so the nested inputs bind properly
            return View(new User { Address = new Address() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ComplexResult(User user)
        {
            // Model binding will populate nested User.Address fields (Address.Street, etc.)
            if (!ModelState.IsValid) return View("ComplexForm", user);
            return View("ComplexResult", user);
        }
    }
}
