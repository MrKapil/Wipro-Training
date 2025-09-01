using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepo.GetAllAsync();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepo.AddAsync(author);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepo.UpdateAsync(author);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorRepo.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
