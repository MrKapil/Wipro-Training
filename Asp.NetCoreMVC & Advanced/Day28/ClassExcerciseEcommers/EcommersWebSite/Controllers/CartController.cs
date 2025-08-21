using EcommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _products;
        private readonly CartService _cart;

        public CartController(ProductService products, CartService cart)
        {
            _products = products;
            _cart = cart;
        }

        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("username");
            return View(_cart.GetCart());
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            var p = _products.Get(id);
            if (p != null) _cart.Add(p);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(int id, int qty)
        {
            _cart.UpdateQuantity(id, qty);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _cart.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
