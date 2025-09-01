using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstEntity.Controllers;

public class EmployeeController : Controller
{
    private readonly YourDbContext _context;
    public EmployeeController(YourDbContext context)
    {
        _context = context;
    }
    //Read

    public IActionReasult Index()
    {
        var customers = _context.Customer.ToList();
        return View(customers);
    }

}


public class CustomerController : Controller
{
    private readonly YourDbContext _context;
    public CustomerController(YourDbContext context)
    {
        _context = context;
    }
    // READ
    public IActionResult Index()
    {
        var customers = _context.Customers.ToList();
        return View(customers);
    }
    // CREATE
    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
 
    // UPDATE
    [HttpPost]
    public IActionResult Edit(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    // DELETE
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
