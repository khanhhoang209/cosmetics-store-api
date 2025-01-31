using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CosmeticsStore.Repositories.Implements;

public class UnitOfWork : IUnitOfWork
{
    private readonly CosmeticsStoreDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ITokenRepository TokenRepository { get; }

    public UnitOfWork(CosmeticsStoreDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        CategoryRepository = new CategoryRepostory(_dbContext);
        ProductRepository = new ProductRepository(_dbContext);
        TokenRepository = new TokenRepository(_dbContext, _configuration);
    }
}