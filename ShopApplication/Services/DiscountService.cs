using ShopApplication.DTOs.Discount;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class DiscountService(IDiscountRepository repository)
{
    public async Task<IReadOnlyList<DiscountDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var discount = await repository.GetAllAsync(cancellationToken);
        return discount.Select(ToDto).ToList();
    }

    public async Task<DiscountDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var discount = await repository.GetByIdAsync(id, cancellationToken)
                       ?? throw new EntityNotFoundException(nameof(Discount), id);

        return ToDto(discount);
    }

    public async Task<DiscountDto> AddAsync(CreateDiscountDto dto, CancellationToken cancellationToken = default)
    {
        var expireDate = DateTimeOffset.UtcNow.AddDays(dto.ExpireDay);
        var discount = new Discount(dto.Code, dto.Percent, expireDate, dto.MaxUse);

        var result = await repository.ExistCodeAsync(discount.Code, cancellationToken);
        if (result)
            throw new DuplicateCodeException(discount.Code);

        await repository.AddAsync(discount, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return ToDto(discount);
    }

    public async Task Remove(long id, CancellationToken cancellationToken = default)
    {
        var discount = await repository.GetByIdAsync(id, cancellationToken)
                       ?? throw new EntityNotFoundException(nameof(Discount), id);

        repository.Remove(discount);
        await repository.SaveChangesAsync(cancellationToken);
    }

    private static DiscountDto ToDto(Discount discount) => new(
        discount.Id,
        discount.Code,
        discount.Percent,
        discount.ExpireDate,
        discount.MaxUse,
        discount.IsActive);
}