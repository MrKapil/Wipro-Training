using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
