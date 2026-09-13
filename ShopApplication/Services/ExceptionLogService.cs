using ShopApplication.DTOs;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class ExceptionLogService(IExceptionLogRepository repository)
{
    public async Task<IReadOnlyList<ExceptionLogDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAllAsync(cancellationToken);
        return result.Select(ToDto).ToList();
    }

    public async Task<ExceptionLogDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetByIdAsync(id, cancellationToken)
                     ?? throw new EntityNotFoundException(nameof(ExceptionLog), id);
        
        return ToDto(result);
    }

    public async Task<IReadOnlyList<ExceptionLogDto>> GetRecentlyAsync(int count,
        CancellationToken cancellationToken = default)
    {
        var result = await repository.GetRecentAsync(count, cancellationToken);
        return result.Select(ToDto).ToList();
    }

    public async Task RemoveAllAsync(CancellationToken cancellationToken = default)
    {
        await repository.RemoveAllAsync(cancellationToken);
    }

    private static ExceptionLogDto ToDto(ExceptionLog log) => new(
        log.Id,
        log.Message,
        log.ExceptionType,
        log.StackTrace,
        log.RequestPath,
        log.HttpMethod,
        log.StatusCode,
        log.CreatedAt
    );
}