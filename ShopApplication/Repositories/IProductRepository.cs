using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IProductRepository : IRepository<Products>
{
    Task<IReadOnlyList<Products>> GetAllAsync(string? searchPhase,long? categoryId,long? maxPrice,CancellationToken cancellationToken = default);
}