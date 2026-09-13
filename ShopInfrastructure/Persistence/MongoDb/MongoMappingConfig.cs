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
            RegisterComments();
            _registered = true;
        }
    }

    private static void RegisterExceptionLog()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(ExceptionLog)))
            return;

        BsonClassMap.RegisterClassMap<ExceptionLog>(map =>
        {
            map.AutoMap();

            map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            map.SetIgnoreExtraElements(true);
        });
    }
    
    private static void RegisterComments()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Comment)))
            return;

        BsonClassMap.RegisterClassMap<Comment>(map =>
        {
            map.AutoMap();
            map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            map.SetIgnoreExtraElements(true);
        });
    }
}
