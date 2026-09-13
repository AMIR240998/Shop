using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class PaymentRepository(ShopDbContext context) : IPaymentRepository
{
    public async Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context.Payments.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<Payment?> GetByOrderIdAsync(long orderId, CancellationToken cancellationToken = default)
        => await context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);


    public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await context.Payments.AddAsync(payment, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}