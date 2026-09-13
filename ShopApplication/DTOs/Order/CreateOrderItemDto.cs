namespace ShopApplication.DTOs.Order;

public record CreateOrderItemDto(
    long ProductId,
    int Quantity);