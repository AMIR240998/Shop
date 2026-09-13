namespace ShopApi.Contracts.Basket;

public record BasketItemResponse(
    long Id,
    long BasketId,
    long ProductId,
    int  Quantity);