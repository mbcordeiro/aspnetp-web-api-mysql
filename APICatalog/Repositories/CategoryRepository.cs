using APICatalog.Context;
using APICatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepostory
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoriesByProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
