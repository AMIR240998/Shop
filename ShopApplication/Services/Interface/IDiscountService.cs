using ShopApplication.DTOs.Discount;

namespace ShopApplication.Services.Interface;

public interface IDiscountService
{
    Task<IReadOnlyList<DiscountDto>> GetAllAsync(CancellationToken cancellationToken = default);


    Task<DiscountDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);


    Task<DiscountDto> AddAsync(CreateDiscountDto dto, CancellationToken cancellationToken = default);


    Task Remove(long id, CancellationToken cancellationToken = default);

}