using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AJAXwithMVC.Models;

namespace AJAXwithMVC.Controllers;

public class StudentController : Controller
{
    private readonly StudentContext _Context;

    public StudentController(studentContext context)
    {
        _Context = context;
    }

    public ActionResult createStudent(Student std)
    {
        context.Student.Add(std);
        context.SaveChanges();
        string message = "SUCCESS";
        return Json(new { Message = message, JsonRequestBehavior.AllowGet });

    }
    public JsonResult GetStudent(string id)
    {
        List<Student> students = new List<Student>();
        students = _Context.Student.ToList();
        return Json(students, JsonRequestBehavior.AlloeGet);
    }
}
