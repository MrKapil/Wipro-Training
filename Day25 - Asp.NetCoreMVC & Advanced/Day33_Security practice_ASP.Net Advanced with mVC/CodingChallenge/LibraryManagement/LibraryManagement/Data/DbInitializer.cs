using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public static class DbInitializer
    {
        public static void Seed(LibraryContext context)
        {
            // quick idempotent seed
            if (context.Authors.Any() || context.Genres.Any() || context.Books.Any())
                return;

            var authors = new List<Author>
            {
                new Author { Name = "George Orwell" },
                new Author { Name = "J.K. Rowling" },
                new Author { Name = "Agatha Christie" }
            };
            context.Authors.AddRange(authors);

            var genres = new List<Genre>
            {
                new Genre { Name = "Fiction" },
                new Genre { Name = "Mystery" },
                new Genre { Name = "Fantasy" },
                new Genre { Name = "Science Fiction" }
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book { Title = "1984", AuthorId = authors[0].AuthorId },
                new Book { Title = "Harry Potter and the Philosopher's Stone", AuthorId = authors[1].AuthorId },
                new Book { Title = "Murder on the Orient Express", AuthorId = authors[2].AuthorId }
            };
            context.Books.AddRange(books);
            context.SaveChanges();

            // link genres (simple examples)
            context.BookGenres.Add(new BookGenre { BookId = books[0].BookId, GenreId = genres.First(g => g.Name == "Science Fiction").GenreId });
            context.BookGenres.Add(new BookGenre { BookId = books[1].BookId, GenreId = genres.First(g => g.Name == "Fantasy").GenreId });
            context.BookGenres.Add(new BookGenre { BookId = books[2].BookId, GenreId = genres.First(g => g.Name == "Mystery").GenreId });
            context.SaveChanges();
        }
    }
}
