namespace ShopApi.Contracts.Discount;

public record CreateDiscountRequest(
    string? Code,
    decimal Percent,
    int ExpireDay,
    short MaxUse);