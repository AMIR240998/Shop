using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<(IReadOnlyList<Product> productsList,int count)> GetAllPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetAllAsync(string? searchPhase,long? categoryId,long? maxPrice,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Product>> UpdatePriceAsync(decimal percent,int categoryId
        ,CancellationToken cancellationToken = default);
}