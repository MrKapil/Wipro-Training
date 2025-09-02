using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureTasks.Data;

namespace SecureTasks.Controllers
{
    [Authorize(Policy = "UserOnly")]
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        public UserController(AppDbContext db) { _db = db; }

        // /User/TaskList (only their tasks)
        public async Task<IActionResult> TaskList()
        {
            var uid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
            var tasks = await _db.Tasks.Where(t => t.OwnerId == uid)
                .OrderByDescending(t => t.CreatedUtc)
                .ToListAsync();
            return View(tasks);
        }

        // Friendly dashboard landing
        public IActionResult Dashboard() => RedirectToAction("TaskList");
    }
}
