using APICatalog.Models;
using APICatalog.Pagination;

namespace APICatalog.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        PagedList<Product> GetProducts(ProductsParameters productsParameters);
        IEnumerable<Product> GetProductsByPrice();
    }
}
