using APICatalog.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalog.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
           _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public List<T> Page<Tipo>(int page, int size)
          where Tipo : class
        {
            return _context.Set<T>()
                .Skip(size * (page - 1))
                  .Take(size).ToList();
        }

        public int Count()
        {
            return _context.Set<T>().AsNoTracking().Count();
        }
    }
}
