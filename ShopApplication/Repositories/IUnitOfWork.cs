using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace ShopApplication.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction>  BeginTransactionAsync(CancellationToken cancellationToken = default);
}