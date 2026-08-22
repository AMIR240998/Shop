namespace ShopInfrastructure.Caching.Redis;

public class RedisSettings
{
    public const string SectionName = "Redis";

    public string ConnectionString { get; set; } = "localhost:6379";
    
    public string InstanceName { get; set; } = "Shop:";
    
    public int DefaultExpirationMinutes { get; set; } = 10;
}