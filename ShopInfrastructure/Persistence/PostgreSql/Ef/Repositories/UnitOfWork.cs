using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using ShopApplication.Repositories;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class UnitOfWork(ShopDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await context.Database.BeginTransactionAsync(cancellationToken);
    }
}