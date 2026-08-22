namespace ShopApplication.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);  
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    void Update(TEntity entity);
    
    void Remove(TEntity entity);
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}