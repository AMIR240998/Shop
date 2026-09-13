using Microsoft.EntityFrameworkCore.Storage;
using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Order?> GetOrderByIdAsync(long orderId,CancellationToken cancellationToken = default);
    
    Task<Order>  AddAsync(Order order, CancellationToken cancellationToken = default);
    
    void Remove(Order order, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}