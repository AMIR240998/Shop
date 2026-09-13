using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class OrderItemRepository(ShopDbContext context) : IOrderItemRepository
{
    public async Task<IReadOnlyList<OrderItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.OrderItems.ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<OrderItem?>> GetAllItemsByIdAsync(long orderId, CancellationToken cancellationToken = default)
        => await context.OrderItems.Where(o => o.OrderId == orderId).ToListAsync(cancellationToken);

    public async Task<OrderItem?> GetItemByIdAsync(long orderId, long itemId, CancellationToken cancellationToken = default)
        => await context.OrderItems.Where(o => o.Id == itemId &&  o.OrderId == orderId).FirstOrDefaultAsync(cancellationToken);

}