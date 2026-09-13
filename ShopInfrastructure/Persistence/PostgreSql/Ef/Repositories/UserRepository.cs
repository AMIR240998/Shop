using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class UserRepository(ShopDbContext context) : RepositoryBase<User>(context),IUserRepository
{
    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users.CountAsync(cancellationToken); 
    }

    public async Task<bool> IsUserNameDuplicate(string username)
        => await DbSet.AnyAsync(c => c.UserName == username);
    
    public async Task<User?> GetByUserName(string username,CancellationToken cancellationToken = default)
        => await DbSet.FirstOrDefaultAsync(c => c.UserName == username, cancellationToken);

    public IQueryable<User> GetAllAsQueryable()
    {
        return context.Users.AsNoTracking();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await context.Database.BeginTransactionAsync(cancellationToken);
    }
}