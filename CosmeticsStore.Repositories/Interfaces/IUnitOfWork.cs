namespace CosmeticsStore.Repositories.Interfaces;

public interface IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; }
}