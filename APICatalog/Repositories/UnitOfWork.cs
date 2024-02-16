using APICatalog.Context;

namespace APICatalog.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductRepostory _productRepostory;
        private CategoryRepository _categoryRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepostory = _productRepostory ?? new ProductRepostory(_context);
            }
        }

        public ICategoryRepostory CategoryRepostory
        {
            get
            {
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
            }
        }

        public async Task Commit()
        {
           await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
