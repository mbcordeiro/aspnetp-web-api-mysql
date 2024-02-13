using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;

namespace APICatalog.Repositories
{
    public class ProductRepostory : Repository<Product>, IProductRepository
    {
        public ProductRepostory(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProducts(ProductsParameters productsParameters)
        {
            return PagedList<Product>.ToPagedList(Get().OrderBy(on => on.ProductId),
         productsParameters.PageNumber, productsParameters.PageSize);
        }

        public IEnumerable<Product> GetProductsByPrice()
        {
            return Get().OrderBy(p => p.Price).ToList();
        }
    }
}
