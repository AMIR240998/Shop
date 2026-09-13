using ShopApplication.Caching.Constants;
using ShopApplication.DTOs;
using ShopApplication.Repositories;

namespace ShopApplication.Caching;

public class ProductCache(ICacheService cacheService)
{
    
    private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(10);

    public async Task<PagedResultDto<ProductDto>> GetAllPagedAsync(
        Func<CancellationToken, Task<(IReadOnlyList<ProductDto> dto, int count)>> function,int pageNumber,int pageSize,
        CancellationToken cancellationToken = default)
    {
          var (dto,count) = await cacheService.GetOrSetAsync(ProductCacheKeyConstant.GetAllPaged(pageNumber,pageSize), 
              function, ExpirationTime,cancellationToken);
          return new PagedResultDto<ProductDto>
          {
              Items = dto,
              PageNumber = pageNumber,
              PageSize = pageSize,
              TotalCount = count
          };
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(
        Func<CancellationToken,Task<IReadOnlyList<ProductDto>>> functionName,CancellationToken cancellationToken = default)
            => await cacheService.GetOrSetAsync(ProductCacheKeyConstant.All,
                  functionName ,ExpirationTime,cancellationToken);
    
    public Task<ProductDto> GetByIdAsync(long id,Func<CancellationToken,Task<ProductDto>> functionName,CancellationToken cancellationToken = default)
        => cacheService.GetOrSetAsync(ProductCacheKeyConstant.ById(id), functionName, ExpirationTime, cancellationToken);
 
    public async Task InvalidateAsync(long id, CancellationToken cancellationToken = default)
    {
        await cacheService.RemoveAsync(ProductCacheKeyConstant.ById(id), cancellationToken);
        await cacheService.RemoveAsync(ProductCacheKeyConstant.All, cancellationToken);
    }
}