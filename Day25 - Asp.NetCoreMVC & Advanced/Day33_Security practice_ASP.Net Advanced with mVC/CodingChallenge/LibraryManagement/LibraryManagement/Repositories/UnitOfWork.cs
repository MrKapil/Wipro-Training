namespace LibraryManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Data.LibraryContext _context;
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
        public IGenreRepository Genres { get; }

        public UnitOfWork(Data.LibraryContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
            Genres = new GenreRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
