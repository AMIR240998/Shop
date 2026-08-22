using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class CustomersRepository(ShopDbContext context) : RepositoryBase<Customers>(context),ICustomerRepository
{
    public async Task<(IReadOnlyList<Customers> customersList,int totalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await context.Customers.CountAsync(cancellationToken);
        var customersList = await context.Customers
            .OrderBy(c => c.Id)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken); 
        
        return (customersList,totalCount);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await context.Customers.CountAsync(cancellationToken); 
    }

    public async Task<bool> IsUserNameDuplicate(string username)
        => await DbSet.AnyAsync(c => c.UserName == username);
    
    public async Task<Customers?> GetByUserName(string username,CancellationToken cancellationToken = default)
        => await DbSet.FirstOrDefaultAsync(c => c.UserName == username, cancellationToken);

    public IQueryable<Customers> GetAllAsQueryable()
    {
        return context.Customers.AsNoTracking();
    }
}