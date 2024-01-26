using APICatalog.Context;
using APICatalog.Models;

namespace APICatalog.Repositories
{
    public class ProductRepostory : Repository<Product>, IProductRepository
    {
        public ProductRepostory(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductsByPrice()
        {
            return Get().OrderBy(p => p.Price).ToList();
        }
    }
}
