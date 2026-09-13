using System.Text.Json;
using Azure.Core.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using ShopApplication.Caching;

namespace ShopInfrastructure.Caching.Redis;

public class DistributedCacheService(IDistributedCache cache,IOptions<RedisSetting> setting) : ICacheService
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    
    private readonly RedisSetting _setting = setting.Value;
    private TimeSpan DefaultTimeout => TimeSpan.FromMinutes(_setting.DefaultExpirationMinutes);
    private string BuildCacheKey(string key) => $"{_setting.InstanceName}{key}";
    
    
    
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
        // var value = await GetAsync<T>(key,cancellationToken);
        // if (value != null)
        //     return value;
        return await functionName(cancellationToken);
        // await SetAsync(key,value,expiration,cancellationToken);
        // return value;
    }
}