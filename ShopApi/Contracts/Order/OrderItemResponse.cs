namespace ShopApi.Contracts.Orders;

public record OrderItemResponse(
    long Id,
    long OrderId,
    long ProductId,
    int Count,
    decimal Price);