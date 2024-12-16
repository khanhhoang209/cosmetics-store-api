using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Interfaces;
using CosmeticsStore.Repositories.Models.Domain;

namespace CosmeticsStore.Repositories.Implements;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(CosmeticsStoreDbContext dbContext) : base(dbContext)
    {
    }
}