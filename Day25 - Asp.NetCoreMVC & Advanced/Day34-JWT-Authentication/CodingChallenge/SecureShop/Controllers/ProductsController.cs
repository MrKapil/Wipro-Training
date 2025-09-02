using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureShop.Data;
using SecureShop.Models;

namespace SecureShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Products.ToList());

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, Authorize(Roles = "Customer")]
public IActionResult Order(int id)
{
    var product = _context.Products.Find(id);
    if (product == null) return NotFound();

    var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    var order = new Order { ProductId = id, UserId = userId };
    _context.Orders.Add(order);
    _context.SaveChanges();

    TempData["Message"] = "Order placed successfully!";
    return RedirectToAction("Index");
}

    }
}
