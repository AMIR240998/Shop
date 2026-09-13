namespace ShopApi.Contracts.Discount;

public record DiscountResponse(
    long Id,
    string Code,
    decimal Percent,
    DateTimeOffset ExpireDate,
    short MaxUse,
    bool IsActive);