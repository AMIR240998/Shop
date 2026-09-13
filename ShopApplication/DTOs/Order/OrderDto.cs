namespace ShopApplication.DTOs.Order;

public record OrderDto(
    long Id,
    long UserId,
    DateTimeOffset? OrderDate,
    string OrderStatus,
    decimal TotalPrice);