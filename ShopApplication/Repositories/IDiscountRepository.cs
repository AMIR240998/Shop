using Microsoft.EntityFrameworkCore.Storage;
using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface IDiscountRepository
{
    Task<IReadOnlyList<Discount>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Discount?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistCodeAsync(string code, CancellationToken cancellationToken = default);
    Task AddAsync(Discount discount, CancellationToken cancellationToken = default);
    void Remove(Discount  discount);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}