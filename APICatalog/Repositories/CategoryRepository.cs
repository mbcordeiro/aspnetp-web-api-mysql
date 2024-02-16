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

        public async Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters)
        {
            return await PagedList<Category>.ToPagedList(Get().OrderBy(on => on.Name),
                                categoriesParameters.PageNumber,
                                categoriesParameters.PageSize);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByProducts()
        {
            return await Get().Include(c => c.Products).ToListAsync();
        }
    }
}
