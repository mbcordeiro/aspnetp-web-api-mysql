using APICatalog.Models;

namespace APICatalog.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByPrice();
    }
}
