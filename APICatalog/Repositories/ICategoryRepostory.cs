using APICatalog.Models;
using APICatalog.Pagination;

namespace APICatalog.Repositories
{
    public interface ICategoryRepostory : IRepository<Category>
    {
        PagedList<Category> GetCategories(CategoriesParameters categoriesParameters);
        IEnumerable<Category> GetCategoriesByProducts();
    }
}
