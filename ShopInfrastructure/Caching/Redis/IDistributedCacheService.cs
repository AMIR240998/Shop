using System.Text.Json;
using Azure.Core.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using ShopApplication.Caching;

namespace ShopInfrastructure.Caching.Redis;

public class DistributedCacheService(IDistributedCache cache,IOptions<RedisSettings> setting) : ICacheService
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private readonly RedisSettings _settings = setting.Value;
    private TimeSpan DefaultTimeout => TimeSpan.FromMinutes(_settings.DefaultExpirationMinutes);
    private string BuildCacheKey(string key) => $"{_settings.InstanceName}{key}";
    
    
    
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value,SerializerOptions);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? DefaultTimeout
        };
        await cache.SetStringAsync(BuildCacheKey(key),json,options, cancellationToken);
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
         var value = await cache.GetStringAsync(BuildCacheKey(key),cancellationToken);
         
         return value == null ? default : JsonSerializer.Deserialize<T>(value, SerializerOptions);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(BuildCacheKey(key), cancellationToken);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> functionName, TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        var value = await GetAsync<T>(BuildCacheKey(key),cancellationToken);
        if (value != null)
            return value;
        value = await functionName(cancellationToken);
        await SetAsync(key,value,expiration,cancellationToken);
        return value;
    }
}