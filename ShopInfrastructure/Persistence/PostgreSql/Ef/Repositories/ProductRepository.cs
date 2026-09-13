using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class ProductRepository(ShopDbContext context) : RepositoryBase<Product>(context), IProductRepository
{
    public async Task<(IReadOnlyList<Product> productsList,int count)> GetAllPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await context.Products.CountAsync(cancellationToken);
        var productsList = await context.Products
            .OrderBy(c => c.Id)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken); 
        
        return (productsList,totalCount);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(string? searchPhase, long? categoryId, long? maxPrice, CancellationToken cancellationToken = default)
    {
        var productsList = await context.Products.ToListAsync(cancellationToken: cancellationToken);
        
        if (!string.IsNullOrEmpty(searchPhase))
            productsList = productsList.Where(p => p.Name.Contains(searchPhase)).ToList();

        if (categoryId is not null)
            productsList = productsList.Where(p => p.CategoryId == categoryId).ToList();
        
        if(maxPrice is not null)
            productsList = productsList.Where(p => p.Price <= maxPrice).ToList();
        return productsList;
    }

    public async Task<IReadOnlyList<Product>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        return await context.Products.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken); 
    }

    public async Task<IReadOnlyList<Product>> UpdatePriceAsync(decimal percent, int categoryId, CancellationToken cancellationToken = default)
    {
        var products = await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync(cancellationToken);
        foreach (var product in products)
        {
            product.ChangePrice(product.Price + (product.Price * percent / 100));
        }
        return products;
    }
}