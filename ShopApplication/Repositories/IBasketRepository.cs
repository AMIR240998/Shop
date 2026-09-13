using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IBasketRepository
{
    Task<Basket?> GetBasketByIdAsync(long id, CancellationToken cancellationToken = default);
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}