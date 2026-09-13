using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class OrderRepository(ShopDbContext context) : RepositoryBase<Order>(context) , IOrderRepository
{
    public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Orders.ToListAsync(cancellationToken);

    public async Task<Order?> GetOrderByIdAsync(long orderId, CancellationToken cancellationToken = default)
        => await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

    public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        return order;
    }

    public void Remove(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Remove(order);
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}