using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureTasks.Data;
using SecureTasks.Models;

namespace SecureTasks.Controllers
{
    [Authorize] // must be signed-in for all task ops
    public class TasksController : Controller
    {
        private readonly AppDbContext _db;
        public TasksController(AppDbContext db) { _db = db; }

        // All tasks visible to Admin; users see own (for convenience page)
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
                return View(await _db.Tasks.OrderByDescending(t => t.CreatedUtc).ToListAsync());

            var uid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
            var mine = await _db.Tasks.Where(t => t.OwnerId == uid).OrderByDescending(t => t.CreatedUtc).ToListAsync();
            return View(mine);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem model)
        {
            if (!ModelState.IsValid) return View(model);

            model.OwnerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
            model.Title = (model.Title ?? "").Trim();
            model.Description = model.Description?.Trim();

            _db.Tasks.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            // Authorization: Admin OR owner with CanEditTask claim
            if (!(User.IsInRole("Admin") || (task.OwnerId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value
                  && User.HasClaim("permission", "CanEditTask"))))
                return Forbid();

            return View(task);
        }

        [HttpPost]
        [Authorize(Policy = "CanEditTaskPolicy")]
        public async Task<IActionResult> Edit(TaskItem model)
        {
            if (!ModelState.IsValid) return View(model);

            var task = await _db.Tasks.FindAsync(model.Id);
            if (task == null) return NotFound();

            // Non-admin can edit only own
            var isOwner = task.OwnerId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
            if (!User.IsInRole("Admin") && !isOwner) return Forbid();

            task.Title = (model.Title ?? "").Trim();
            task.Description = model.Description?.Trim();

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
