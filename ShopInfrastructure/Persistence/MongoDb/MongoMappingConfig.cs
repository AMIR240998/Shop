using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.MongoDb;

public static class MongoMappingConfig
{
    private static bool _registered;
    private static readonly Lock Lock = new();

    public static void Register()
    {
        lock (Lock)
        {
            if (_registered)
                return;

            RegisterExceptionLog();
            _registered = true;
        }
    }

    private static void RegisterExceptionLog()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(ExceptionLogs)))
            return;

        BsonClassMap.RegisterClassMap<ExceptionLogs>(map =>
        {
            map.AutoMap();

            map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            map.SetIgnoreExtraElements(true);
        });
    }
}