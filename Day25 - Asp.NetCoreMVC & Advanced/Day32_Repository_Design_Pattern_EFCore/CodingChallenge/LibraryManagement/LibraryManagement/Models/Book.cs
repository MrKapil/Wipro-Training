using System.Collections.Generic;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
