using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Interfaces;

namespace CosmeticsStore.Repositories.Implements;

public class UnitOfWork : IUnitOfWork
{
    private readonly CosmeticsStoreDbContext _dbContext;

    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(CosmeticsStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        CategoryRepository = new CategoryRepostory(_dbContext);
        ProductRepository = new ProductRepository(_dbContext);
    }


}