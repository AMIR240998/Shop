using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ShopApplication.Caching;
using StackExchange.Redis;

namespace ShopInfrastructure.Caching.Redis;

public class RedisCachService(RedisConnection connection,IOptions<RedisSetting> settings) : ICacheService
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    
    private readonly RedisSetting _setting = settings.Value;
    
    private readonly IDatabase _db = connection.Database; 
    
    
    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var payload = JsonSerializer.Serialize(value, SerializerOptions);
        return _db.StringSetAsync(BuildKey(key), payload, expiration ?? DefaultExpiration);
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _db.StringGetAsync(BuildKey(key));
        return value.IsNullOrEmpty
            ? default 
            : JsonSerializer.Deserialize<T>(value.ToString(), SerializerOptions);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => _db.KeyDeleteAsync(BuildKey(key));

    public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        => _db.KeyExistsAsync(BuildKey(key));

    public async Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> functionName, TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        var cached = await GetAsync<T>(key,cancellationToken);
        if (cached != null)
            return cached;
        var value = await functionName(cancellationToken);
        await SetAsync(key,value,expiration ?? DefaultExpiration,cancellationToken);
        return value;
    }

    private TimeSpan DefaultExpiration => TimeSpan.FromMinutes(_setting.DefaultExpirationMinutes);
    private string BuildKey(string key) => $"{_setting.InstanceName}{key}";
}