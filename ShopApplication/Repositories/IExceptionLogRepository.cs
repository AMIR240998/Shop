using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IExceptionLogRepository
{
    Task AddAsync(ExceptionLog log, CancellationToken cancellationToken = default);
    
    Task<ExceptionLog?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<ExceptionLog>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<ExceptionLog>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
    
    Task RemoveAllAsync(CancellationToken cancellationToken = default);
}