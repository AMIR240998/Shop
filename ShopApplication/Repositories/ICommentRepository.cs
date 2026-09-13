using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface ICommentRepository
{
     Task<IReadOnlyList<Comment>> GetAllAsync(CancellationToken cancellationToken = default);
     
     Task<IReadOnlyList<Comment>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
     
     Task<IReadOnlyList<Comment>> GetAllByCustomerIdAsync(long customerId, CancellationToken cancellationToken = default);
    
    Task<Comment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    
     Task AddAsync(Comment entity, CancellationToken cancellationToken = default);

     Task Update(Comment entity);

     Task Remove(string id,CancellationToken cancellationToken = default);
}