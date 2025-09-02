using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required, StringLength(500)]
        public string Title { get; set; } = null!;

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
