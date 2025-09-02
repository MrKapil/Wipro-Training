using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [Required, StringLength(500)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        public List<int> SelectedGenreIds { get; set; } = new List<int>();

        // For dropdowns / checklists in the view
        public IEnumerable<SelectListItem> Authors { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Genres { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
