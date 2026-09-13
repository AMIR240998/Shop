using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class CategoryRepository(ShopDbContext context) : RepositoryBase<Category>(context),ICategoryRepository
{
    public async void RemoveAllAsync(CancellationToken cancellationToken = default)
    {
        await context.Categories.ExecuteDeleteAsync(cancellationToken);
    }
}