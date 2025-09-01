using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IGenreRepository _genreRepo;

        public BookController(IBookRepository bookRepo, IAuthorRepository authorRepo, IGenreRepository genreRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _genreRepo = genreRepo;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepo.GetAllAsync();
            return View(books);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorRepo.GetAllAsync();
            ViewBag.Genres = await _genreRepo.GetAllAsync();
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<IActionResult> Create(Book book, int[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                book.Genres = new List<Genre>();
                foreach (var genreId in selectedGenres)
                {
                    var genre = await _genreRepo.GetByIdAsync(genreId);
                    if (genre != null) book.Genres.Add(genre);
                }

                await _bookRepo.AddAsync(book);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data" });
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();

            ViewBag.Authors = await _authorRepo.GetAllAsync();
            ViewBag.Genres = await _genreRepo.GetAllAsync();
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Book book, int[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                book.Genres = new List<Genre>();
                foreach (var genreId in selectedGenres)
                {
                    var genre = await _genreRepo.GetByIdAsync(genreId);
                    if (genre != null) book.Genres.Add(genre);
                }

                await _bookRepo.UpdateAsync(book);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data" });
        }

        // POST: Book/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookRepo.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
