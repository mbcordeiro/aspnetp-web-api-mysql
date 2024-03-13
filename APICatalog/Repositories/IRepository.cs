using System.Linq.Expressions;

namespace APICatalog.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        List<T> Page<Tipo>
        (int page, int size) where Tipo : class;

        int Count();
    }
}
