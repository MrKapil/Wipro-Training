using BookStoreApp.Models;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly DbFactory _dbFactory;

        public BookRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = new List<Book>();

            using SqlConnection conn = _dbFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_Books_GetAll", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                books.Add(new Book
                {
                    BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Author = reader.GetString(reader.GetOrdinal("Author")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Isbn = reader.GetString(reader.GetOrdinal("Isbn")),
                    PublishedOn = reader.IsDBNull(reader.GetOrdinal("PublishedOn"))
                                  ? null : reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                    Genre = reader.IsDBNull(reader.GetOrdinal("Genre"))
                                  ? null : reader.GetString(reader.GetOrdinal("Genre")),
                    Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                });
            }

            return books;
        }

        public Book? GetById(int id)
        {
            using SqlConnection conn = _dbFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_Books_GetById", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@BookId", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Book
                {
                    BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Author = reader.GetString(reader.GetOrdinal("Author")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Isbn = reader.GetString(reader.GetOrdinal("Isbn")),
                    PublishedOn = reader.IsDBNull(reader.GetOrdinal("PublishedOn"))
                                  ? null : reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                    Genre = reader.IsDBNull(reader.GetOrdinal("Genre"))
                                  ? null : reader.GetString(reader.GetOrdinal("Genre")),
                    Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                };
            }

            return null;
        }

        public void Add(Book book)
        {
            using SqlConnection conn = _dbFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_Books_Insert", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@Price", book.Price);
            cmd.Parameters.AddWithValue("@Isbn", book.Isbn);
            cmd.Parameters.AddWithValue("@PublishedOn", (object?)book.PublishedOn ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Genre", (object?)book.Genre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Stock", book.Stock);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(Book book)
        {
            using SqlConnection conn = _dbFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_Books_Update", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@BookId", book.BookId);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@Price", book.Price);
            cmd.Parameters.AddWithValue("@Isbn", book.Isbn);
            cmd.Parameters.AddWithValue("@PublishedOn", (object?)book.PublishedOn ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Genre", (object?)book.Genre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Stock", book.Stock);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using SqlConnection conn = _dbFactory.CreateConnection();
            using var cmd = new SqlCommand("usp_Books_Delete", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@BookId", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
