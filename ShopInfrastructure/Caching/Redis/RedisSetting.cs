namespace ShopInfrastructure.Caching.Redis;

public class RedisSetting
{
    public const string SectionName = "Redis";

    public string ConnectionString { get; set; } = "localhost:6379";
    
    public string InstanceName { get; set; } = "Shop:";
    
    public int DefaultExpirationMinutes { get; set; } = 10;
}