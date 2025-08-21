using Microsoft.AspNetCore.Mvc;
using ASP.NetCoreMVCApp.Models;

namespace ASP.NetCoreMVCApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            Employee e1 = new Employee()
            {
                Empid = 101,
                Fname = "Prime",
                Lname = "Optimus",
                city = "Cybertron"
            };
            return View(e1);
        }
        public IActionResult IBMView()
        {
            Employee e1 = new Employee()
            {
                Empid = 101,
                Fname = "Prime",
                Lname = "Optimus",
                city = "Cybertron"
            };
            return View(e1);
        }
        public IActionResult create_Employee() {
            return View();
        }
        public IActionResult Edit_Employee() {
            return View();
        }
        public IActionResult Delete_Employee() {
            return View();
        }
       
    }
}
