using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepo;

        public GenreController(IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepo.GetAllAsync();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepo.AddAsync(genre);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _genreRepo.GetByIdAsync(id);
            if (genre == null) return NotFound();
            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepo.UpdateAsync(genre);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreRepo.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
