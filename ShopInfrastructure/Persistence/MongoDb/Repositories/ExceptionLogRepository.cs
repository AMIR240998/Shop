using MongoDB.Driver;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.MongoDb.Repositories;

public class ExceptionLogRepository(MongoContext context) : IExceptionLogRepository
{
    private IMongoCollection<ExceptionLog> Collection => context.ExceptionLogs;
    
    public Task AddAsync(ExceptionLog log, CancellationToken cancellationToken = default)
       => Collection.InsertOneAsync(log,options: null, cancellationToken);

    public async Task<ExceptionLog?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
       => await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<ExceptionLog>> GetAllAsync(CancellationToken cancellationToken = default)
       => await Collection.Find(_ => true).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<ExceptionLog>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
       => await Collection.Find(Builders<ExceptionLog>.Filter.Empty)
          .SortByDescending(x => x.CreatedAt)
          .Limit(count)
          .ToListAsync(cancellationToken);

    public Task RemoveAllAsync(CancellationToken cancellationToken = default)
    {
       var filter = Builders<ExceptionLog>.Filter.Empty;
       return Collection.DeleteManyAsync(filter, cancellationToken);
    }
}