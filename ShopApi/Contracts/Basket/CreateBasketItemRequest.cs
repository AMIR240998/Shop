namespace ShopApi.Contracts.Basket;

public record CreateBasketItemRequest(
    long BasketId,
    long ProductId,
    int Quantity);