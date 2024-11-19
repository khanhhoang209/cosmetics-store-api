using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Interfaces;

namespace CosmeticsStore.Repositories.Implements;

public class UnitOfWork : IUnitOfWork
{
    private readonly CosmeticsStoreDbContext _dbContext;

    public ICategoryRepository CategoryRepository { get; }

    public UnitOfWork(CosmeticsStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        CategoryRepository = new CategoryRepostory(_dbContext);
    }


}