using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IExceptionLogRepository
{
    Task AddAsync(ExceptionLogs log, CancellationToken cancellationToken = default);
    
    Task<ExceptionLogs?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<ExceptionLogs>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<ExceptionLogs>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
}