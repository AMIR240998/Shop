using ShopApplication.Caching.Constants;
using ShopApplication.DTOs;
using ShopApplication.Repositories;

namespace ShopApplication.Caching;

public class ProductCache(ICacheService cacheService)
{
    
    private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(10);
    
    public Task<IReadOnlyList<ProductDto>> GetAllAsync(
        Func<CancellationToken,Task<IReadOnlyList<ProductDto>>> functionName,CancellationToken cancellationToken = default)
            => cacheService.GetOrSetAsync(ProductsCacheKeyConstants.All,
                  functionName ,ExpirationTime,cancellationToken);
    
    public Task<ProductDto> GetByIdAsync(long id,Func<CancellationToken,Task<ProductDto>> functionName,CancellationToken cancellationToken = default)
        => cacheService.GetOrSetAsync(ProductsCacheKeyConstants.ById(id), functionName, ExpirationTime, cancellationToken);
 
    public async Task InvalidateAsync(long id, CancellationToken cancellationToken = default)
    {
        await cacheService.RemoveAsync(ProductsCacheKeyConstants.ById(id), cancellationToken);
        await cacheService.RemoveAsync(ProductsCacheKeyConstants.All, cancellationToken);
    }
}