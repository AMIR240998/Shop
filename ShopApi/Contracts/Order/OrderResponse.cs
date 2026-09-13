namespace ShopApi.Contracts.Order;

public record OrderResponse(
    long Id,
    long UserId,
    DateTimeOffset? OrderDate,
    string OrderStatus,
    decimal TotalPrice);