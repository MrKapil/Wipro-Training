using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Repositories
{        public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryContext context) : base(context) { }
    }
}
