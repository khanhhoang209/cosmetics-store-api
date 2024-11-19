using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Interfaces;
using CosmeticsStore.Repositories.Models.Domain;

namespace CosmeticsStore.Repositories.Implements;

public class CategoryRepostory : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepostory(CosmeticsStoreDbContext dbContext) : base(dbContext)
    {
    }
}