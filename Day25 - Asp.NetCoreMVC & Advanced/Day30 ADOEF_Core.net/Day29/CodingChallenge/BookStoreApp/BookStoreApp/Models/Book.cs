namespace BookStoreApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public DateTime? PublishedOn { get; set; }
        public string? Genre { get; set; }
        public int Stock { get; set; }
    }
}

