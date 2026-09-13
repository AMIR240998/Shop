using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IFavoriteRepository
{
    Task<IReadOnlyList<Favorite>> GetAllAsync(CancellationToken cancellationToken =  default);
    Task<Favorite?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task AddAsync(Favorite favorite, CancellationToken cancellationToken =  default);
    void RemoveAsync(Favorite favorite);
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
}