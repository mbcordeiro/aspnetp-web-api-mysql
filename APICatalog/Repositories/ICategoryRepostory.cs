using APICatalog.Models;
using APICatalog.Pagination;

namespace APICatalog.Repositories
{
    public interface ICategoryRepostory : IRepository<Category>
    {
        Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters);
        Task<IEnumerable<Category>> GetCategoriesByProducts();
    }
}
