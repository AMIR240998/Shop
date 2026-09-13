using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class BasketRepository(ShopDbContext context) : IBasketRepository
{
    public async Task<Basket?> GetBasketByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context.Baskets.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}