using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> GetByIdWithAuthorAndGenresAsync(int id);
        Task<IEnumerable<Book>> GetAllWithAuthorAndGenresAsync();
    }
}
