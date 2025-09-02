using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LibraryManagement.Data;

namespace LibraryManagement.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(LibraryContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
                dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
