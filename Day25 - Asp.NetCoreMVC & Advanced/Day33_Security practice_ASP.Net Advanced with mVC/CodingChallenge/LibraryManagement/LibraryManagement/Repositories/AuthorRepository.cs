using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context) { }
    }
}
