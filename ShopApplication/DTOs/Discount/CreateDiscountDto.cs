namespace ShopApplication.DTOs.Discount;

public record CreateDiscountDto(
    string? Code,
    decimal Percent,
    int ExpireDay,
    short MaxUse);