using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class DiscountRepository(ShopDbContext context) : IDiscountRepository
{
    public async Task<IReadOnlyList<Discount>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Discounts.ToListAsync(cancellationToken);

    public async Task<Discount?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
     => await context.Discounts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<bool> ExistCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await context.Discounts.AnyAsync(c => c.Code == code, cancellationToken);
    }

    public async Task AddAsync(Discount discount, CancellationToken cancellationToken = default)
    {
        await context.Discounts.AddAsync(discount);
    }

    public void Remove(Discount discount)
    {
        context.Discounts.Remove(discount);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}