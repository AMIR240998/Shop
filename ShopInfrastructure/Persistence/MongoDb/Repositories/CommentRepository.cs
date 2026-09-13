using MongoDB.Driver;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.MongoDb.Repositories;

public class CommentRepository(MongoContext context) : ICommentRepository
{
    private IMongoCollection<Comment>  Collection => context.Comments;

    public async Task<IReadOnlyList<Comment>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Collection.Find(_ => true).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Comment>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
        => await Collection.Find(x => x.ProductId == productId).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Comment>> GetAllByCustomerIdAsync(long userId, CancellationToken cancellationToken = default)
        => await Collection.Find(x => x.UserId == userId).ToListAsync(cancellationToken);
    
    public async Task<Comment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        => await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public Task AddAsync(Comment entity, CancellationToken cancellationToken = default)
        => Collection.InsertOneAsync(entity,options:null,cancellationToken);

    public async Task Update(Comment entity)
    {
        var filter = Builders<Comment>.Filter.Eq(x => x.Id, entity.Id);

        var update = Builders<Comment>.Update.Set(x => x.Text, entity.Text)
            .Set(x => x.Rate, entity.Rate);

        await Collection.UpdateOneAsync(filter, update);
    }

    public async Task Remove(string id,CancellationToken cancellationToken = default)
    {
        var filter = Builders<Comment>.Filter.Eq(x => x.Id, id);
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }
}