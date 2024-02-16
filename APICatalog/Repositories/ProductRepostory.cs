using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class ProductRepostory : Repository<Product>, IProductRepository
    {
        public ProductRepostory(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Product>> GetProducts(ProductsParameters productsParameters)
        {
            return await PagedList<Product>.ToPagedList(Get().OrderBy(on => on.ProductId),
         productsParameters.PageNumber, productsParameters.PageSize);
        }   

        public async Task<IEnumerable<Product>> GetProductsByPrice()
        {
            return await Get().OrderBy(p => p.Price).ToListAsync();
        }
    }
}
