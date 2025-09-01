using Microsoft.AspNetCore.Mvc;

namespace SimpleAuthDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("User");
            if (user == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.Username = user;
            return View();
        }
    }
}
