using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class ProductsRepository(ShopDbContext context) : RepositoryBase<Products>(context), IProductRepository
{
    public async Task<IReadOnlyList<Products>> GetAllAsync(string? searchPhase, long? categoryId, long? maxPrice, CancellationToken cancellationToken = default)
    {
        var productsList = await context.Products.ToListAsync(cancellationToken: cancellationToken);
        
        if (!string.IsNullOrEmpty(searchPhase))
            productsList = productsList.Where(p => searchPhase.Contains(p.Name)).ToList();

        if (categoryId is not null)
            productsList = productsList.Where(p => p.CategoryId == categoryId).ToList();
        
        if(maxPrice is not null)
            productsList = productsList.Where(p => p.Price <= maxPrice).ToList();
        return productsList;
    }
}