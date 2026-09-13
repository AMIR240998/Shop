using ShopApi.Contracts.Discount;
using ShopApplication.DTOs.Discount;
using ShopDomain.Entities;

namespace ShopApi.Mappers;

public static class DiscountContractMapping
{
    public static DiscountResponse ToResponse(this DiscountDto dto) => new(
        dto.Id,
        dto.Code,
        dto.Percent,
        dto.ExpireDate,
        dto.MaxUse,
        dto.IsActive);

    public static CreateDiscountDto CreateToDto(this CreateDiscountRequest request) => new(
        request.Code,
        request.Percent,
        request.ExpireDay,
        request.MaxUse);
}