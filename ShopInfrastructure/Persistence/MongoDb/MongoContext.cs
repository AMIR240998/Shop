using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.MongoDb;

public class MongoContext
{
    private readonly MongoSetting _settings;

    public MongoContext(IOptions<MongoSetting> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        Database = client.GetDatabase(_settings.Database);
    }

    private IMongoDatabase Database { get; }

    public IMongoCollection<ExceptionLogs> ExceptionLogs =>
        Database.GetCollection<ExceptionLogs>(_settings.ExceptionLogsCollection);
}