using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface ICustomerRepository : IRepository<Customers>
{
    Task<(IReadOnlyList<Customers> customersList,int totalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<bool> IsUserNameDuplicate(string username);
    Task<Customers?> GetByUserName(string username,CancellationToken cancellationToken = default);
    
    IQueryable<Customers> GetAllAsQueryable();
}