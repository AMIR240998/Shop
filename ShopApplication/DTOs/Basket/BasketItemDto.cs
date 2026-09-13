namespace ShopApplication.DTOs.Basket;

public record BasketItemDto(
    long Id,
    long BasketId,
    long ProductId,
    int  Quantity);