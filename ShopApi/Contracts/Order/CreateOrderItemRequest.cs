namespace ShopApi.Contracts.Order;

public record CreateOrderItemRequest(
    long ProductId,
    int Count);