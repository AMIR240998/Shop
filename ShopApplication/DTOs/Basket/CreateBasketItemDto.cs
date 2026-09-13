namespace ShopApplication.DTOs.Basket;

public record CreateBasketItemDto(
    long BasketId,
    long ProductId,
    int Quantity);