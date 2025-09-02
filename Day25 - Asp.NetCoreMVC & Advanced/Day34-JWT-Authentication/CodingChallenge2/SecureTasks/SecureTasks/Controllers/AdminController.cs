using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureTasks.Data;

namespace SecureTasks.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        // /Admin/ManageTasks
        public async Task<IActionResult> ManageTasks()
        {
            var tasks = await _db.Tasks
                .OrderByDescending(t => t.CreatedUtc)
                .ToListAsync();
            return View(tasks);
        }
    }
}
