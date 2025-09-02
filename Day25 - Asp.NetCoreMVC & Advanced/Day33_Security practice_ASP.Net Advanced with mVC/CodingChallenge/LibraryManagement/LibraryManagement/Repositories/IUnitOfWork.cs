using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IGenreRepository Genres { get; }
        Task<int> CompleteAsync();
    }
}
