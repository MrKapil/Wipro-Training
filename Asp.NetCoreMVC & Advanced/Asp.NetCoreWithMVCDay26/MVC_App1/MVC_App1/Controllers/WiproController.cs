using Microsoft.AspNetCore.Mvc;

namespace MVC_App1.Controllers
{
    public class WiproController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HelperTag_Demo()
        {
            return View();
        }
        public IActionResult Std_Helper_Demo() { 
            return View();
        }
        public IActionResult Helper_Demo_UsingModel()
        {
            return View();
        }

        public IActionResult ClientSide_UserView() {
            return View();
        }
    }
}
