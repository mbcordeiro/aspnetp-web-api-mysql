using APICatalog.Models;
using APICatalog.Pagination;

namespace APICatalog.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetProducts(ProductsParameters productsParameters);
        Task<IEnumerable<Product>> GetProductsByPrice();
    }
}
