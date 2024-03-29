﻿namespace APICatalog.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepostory CategoryRepostory { get; }
        Task Commit();
    }
}
