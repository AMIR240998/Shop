namespace ShopApplication.DTOs.Discount;

public record DiscountDto(
    long Id,
    string Code,
    decimal Percent,
    DateTimeOffset ExpireDate,
    short MaxUse,
    bool IsActive);