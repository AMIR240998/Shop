using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IOrderItemRepository
{
    Task<IReadOnlyList<OrderItem>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<OrderItem?>> GetAllItemsByIdAsync(long orderId, CancellationToken cancellationToken = default);
    
    Task<OrderItem?> GetItemByIdAsync(long orderId, long itemId, CancellationToken cancellationToken = default);
    
}