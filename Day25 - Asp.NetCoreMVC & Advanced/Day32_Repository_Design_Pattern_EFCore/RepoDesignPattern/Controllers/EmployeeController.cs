using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RepoDesignPattern.Repository;
using RepoDesignPattern.Models;
using RepoDesignPattern.Data;

namespace RepoDesignPattern.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        public ActionResult AddEmployee()
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Add Employee Failed";
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                int result = _employeeRepository.AddEmployee(model);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    TempData["Failed"] = "Failed";
                    return RedirectToAction("AddEmployee", "Employee");
                }
            }
            return View();
        }

        public ActionResult EditEmployee(int employeeId)
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Edit Employee Failed";
            }
            Employee model = _employeeRepository.GetEmployeeById(employeeId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                int result = _employeeRepository.UpdateEmployee(model);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }
            }
            return View();
        }

        public ActionResult DeleteEmployee(int employeeId)
        {
            Employee model = _employeeRepository.GetEmployeeById(employeeId);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteEmployee(Employee model)
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = " delete employee failed";
            }
            _employeeRepository.DeleteEmployee(model.EmployeeId);
            return RedirectToAction("Index", "Employee");
        }
    }
}