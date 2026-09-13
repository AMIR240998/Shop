using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class FavoriteRepository(ShopDbContext context) : IFavoriteRepository
{
    public async Task<IReadOnlyList<Favorite>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Favorites.ToListAsync(cancellationToken);

    public async Task<Favorite?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context.Favorites.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

    public async Task AddAsync(Favorite favorite, CancellationToken cancellationToken = default)
    {
        await context.Favorites.AddAsync(favorite, cancellationToken);
    }

    public void RemoveAsync(Favorite favorite)
    { 
        context.Favorites.Remove(favorite);
    }

    public Task SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        context.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
    }
}