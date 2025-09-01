using EFCodeFirstDemo;
using EFCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with dependency injection
builder.Services.AddDbContext<EFCodeFirstContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Run your EF Core logic inside a scoped service
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EFCodeFirstContext>();

    var studentAddress = new StudentAddress
    {
        AddressLine1 = "Text1",
        AddressLine2 = "Text2"
    };

    var student = new Student
    {
        FirstName = "Pranaya",
        LastName = "Rout",
        Address = studentAddress
    };

    context.Students.Add(student);
    context.SaveChanges();

    Console.WriteLine("Student Added");
}

app.Run();