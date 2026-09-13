using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    void RemoveAllAsync(CancellationToken cancellationToken = default);
}