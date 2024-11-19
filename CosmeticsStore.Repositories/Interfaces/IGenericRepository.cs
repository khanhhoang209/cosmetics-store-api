using System.Linq.Expressions;

namespace CosmeticsStore.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    List<T> GetAll();
    (IEnumerable<T>, int) GetFilter(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Func<IQueryable<T>, IQueryable<T>>[]? includes);

    bool Create(T entity);
    bool Create(IEnumerable<T> entities);
    bool Update(T entity);
    bool Remove(T entity);
    void Detach(T entity);
    T? GetById(int id);
    T? GetById(string code);
    T? GetById(Guid code);

// Asynchronous methods
    Task<List<T>> GetAllAsync();
    Task<(IEnumerable<T>, int)> GetFilterAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Func<IQueryable<T>, IQueryable<T>>[]? includes);

    Task<bool> CreateAsync(T entity);
    Task<bool> CreateAsync(IEnumerable<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> RemoveAsync(T entity);

    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(string code);
    Task<T?> GetByIdAsync(Guid code);


    void PrepareCreate(T entity);
    void PrepareCreate(IEnumerable<T> entities);
    void PrepareUpdate(T entity);
    bool PrepareUpdate(IEnumerable<T> entities);
    void PrepareRemove(T entity);
    bool Save();
    Task<bool> SaveAsync();
}
