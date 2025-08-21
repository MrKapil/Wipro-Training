using System.Text.Json;
using EcommerceApp.Models;
using EcommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly CartService _cart;
        public OrderController(CartService cart) => _cart = cart;

        public IActionResult Checkout()
        {
            var items = _cart.GetCart();
            if (items.Count == 0) return RedirectToAction("Index", "Home");

            ViewBag.User = HttpContext.Session.GetString("username") ?? "Guest";
            return View(items);
        }

        [HttpPost]
        public IActionResult Place()
        {
            var items = _cart.GetCart();
            if (items.Count == 0) return RedirectToAction("Index", "Home");

            var order = new Order
            {
                Customer = HttpContext.Session.GetString("username") ?? "Guest",
                Items = items
            };

            // keep last order in TempData to show on Razor Page
            TempData["LastOrder"] = JsonSerializer.Serialize(order);
            _cart.Clear();

            // Razor Page mapped at /order/bill (custom route)
            return Redirect("/order/bill");
        }
    }
}
