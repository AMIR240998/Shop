using Microsoft.EntityFrameworkCore.Storage;
using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsUserNameDuplicate(string username);
    Task<User?> GetByUserName(string username,CancellationToken cancellationToken = default);
    
    IQueryable<User> GetAllAsQueryable();
    
    Task<IDbContextTransaction>  BeginTransactionAsync(CancellationToken cancellationToken = default);
    
}