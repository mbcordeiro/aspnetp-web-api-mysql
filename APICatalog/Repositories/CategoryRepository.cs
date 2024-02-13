using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepostory
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Category> GetCategories(CategoriesParameters categoriesParameters)
        {
            return PagedList<Category>.ToPagedList(Get().OrderBy(on => on.Name),
                                categoriesParameters.PageNumber,
                                categoriesParameters.PageSize);

            public IEnumerable<Category> GetCategoriesByProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
