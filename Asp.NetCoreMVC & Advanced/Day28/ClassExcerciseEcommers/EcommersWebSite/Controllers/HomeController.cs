using EcommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _products;
        public HomeController(ProductService products) => _products = products;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") is null)
                return RedirectToAction("Login", "Account");

            ViewBag.User = HttpContext.Session.GetString("username");
            return View(_products.GetAll());
        }
    }
}
