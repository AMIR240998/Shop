namespace ShopApplication.DTOs.Order;

public record OrderItemDto(
    long Id,
    long OrderId,
    long ProductId,
    int Count,
    decimal Price);