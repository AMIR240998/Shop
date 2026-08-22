using MongoDB.Driver;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.MongoDb.Repositories;

public class ExceptionLogRepository(MongoContext context) : IExceptionLogRepository
{
    private IMongoCollection<ExceptionLogs> Collection => context.ExceptionLogs;
    
    public Task AddAsync(ExceptionLogs log, CancellationToken cancellationToken = default)
       => Collection.InsertOneAsync(log,options: null, cancellationToken);

    public async Task<ExceptionLogs?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
       => await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<ExceptionLogs>> GetAllAsync(CancellationToken cancellationToken = default)
       => await Collection.Find(_ => true).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<ExceptionLogs>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
       => await Collection.Find(Builders<ExceptionLogs>.Filter.Empty)
          .SortByDescending(x => x.CreatedAt)
          .Limit(count)
          .ToListAsync(cancellationToken);
}