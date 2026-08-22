using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ShopInfrastructure.Caching.Redis;
//StackExchangeRedis
public class RedisConnection(IOptions<RedisSettings> settings) : IDisposable
{
    private readonly ConnectionMultiplexer _connection = 
        ConnectionMultiplexer.Connect(settings.Value.ConnectionString);

    public IDatabase Database => _connection.GetDatabase();
    
    public void Dispose() => _connection.Dispose();
}