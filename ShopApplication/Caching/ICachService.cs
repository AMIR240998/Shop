namespace ShopApplication.Caching;

public interface ICacheService
{
    Task SetAsync<T>(string key,T value,TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    
    Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken = default);
    
    Task RemoveAsync(string key,CancellationToken cancellationToken = default);

    Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> functionName, TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);
}