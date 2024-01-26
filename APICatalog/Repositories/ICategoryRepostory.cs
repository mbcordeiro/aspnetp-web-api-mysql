using APICatalog.Models;

namespace APICatalog.Repositories
{
    public interface ICategoryRepostory : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesByProducts();
    }
}
