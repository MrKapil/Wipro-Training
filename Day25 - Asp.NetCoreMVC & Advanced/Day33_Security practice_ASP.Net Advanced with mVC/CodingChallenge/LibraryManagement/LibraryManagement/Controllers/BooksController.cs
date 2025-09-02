using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _uow;
        public BooksController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _uow.Books.GetAllWithAuthorAndGenresAsync();
            return View(books);
        }

        // Partial: Book list (for AJAX refresh)
        public async Task<IActionResult> BookListPartial()
        {
            var books = await _uow.Books.GetAllWithAuthorAndGenresAsync();
            return PartialView("_BookListPartial", books);
        }

        // GET: Create (returns partial form)
        public IActionResult Create()
        {
            var vm = new BookViewModel
            {
                Authors = GetAuthorsSelectList(),
                Genres = GetGenresSelectList()
            };
            return PartialView("_CreateEditBookPartial", vm);
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Authors = GetAuthorsSelectList();
                vm.Genres = GetGenresSelectList();
                return PartialView("_CreateEditBookPartial", vm);
            }

            try
            {
                var book = new Book
                {
                    Title = vm.Title,
                    AuthorId = vm.AuthorId
                };

                foreach (var gid in vm.SelectedGenreIds.Distinct())
                {
                    book.BookGenres.Add(new BookGenre { GenreId = gid });
                }

                await _uow.Books.AddAsync(book);
                await _uow.CompleteAsync();
                return Json(new { success = true, message = "Book created" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Edit partial
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _uow.Books.GetByIdWithAuthorAndGenresAsync(id);
            if (book == null) return NotFound();

            var vm = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                SelectedGenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList(),
                Authors = GetAuthorsSelectList(),
                Genres = GetGenresSelectList()
            };

            return PartialView("_CreateEditBookPartial", vm);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Authors = GetAuthorsSelectList();
                vm.Genres = GetGenresSelectList();
                return PartialView("_CreateEditBookPartial", vm);
            }

            try
            {
                var book = await _uow.Books.GetByIdWithAuthorAndGenresAsync(vm.BookId);
                if (book == null) return Json(new { success = false, message = "Book not found" });

                book.Title = vm.Title;
                book.AuthorId = vm.AuthorId;

                // update genres: simple approach remove old, add new
                book.BookGenres.Clear();
                foreach (var gid in vm.SelectedGenreIds.Distinct())
                {
                    book.BookGenres.Add(new BookGenre { BookId = book.BookId, GenreId = gid });
                }

                _uow.Books.Update(book);
                await _uow.CompleteAsync();
                return Json(new { success = true, message = "Book updated" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _uow.Books.DeleteAsync(id);
                await _uow.CompleteAsync();
                return Json(new { success = true, message = "Deleted" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Helpers
        private IEnumerable<SelectListItem> GetAuthorsSelectList()
        {
            var authors = _uow.Authors.GetAllAsync().Result; // small sync-over-async for convenience in viewmodels
            return authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = a.Name });
        }

        private IEnumerable<SelectListItem> GetGenresSelectList()
        {
            var genres = _uow.Genres.GetAllAsync().Result;
            return genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.Name });
        }
    }
}
