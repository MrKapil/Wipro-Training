using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Linq;

namespace MVC_App1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details() {

            var product = new { Id = id, Name = "Sample Product" };


            ViewBag.prdid = product.Id;
            ViewBag.product = product.Name;

            return View(product);
        }
    }
}
