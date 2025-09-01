using Microsoft.AspNetCore.Mvc;

namespace RegistrationForm.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegistrationForm() { 
            return View();
        }

    }
}
