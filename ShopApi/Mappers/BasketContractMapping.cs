using ShopApi.Contracts.Basket;
using ShopApplication.DTOs.Basket;

namespace ShopApi.Mappers;

public static class BasketContractMapping
{
    public static BasketItemResponse ToResponse(this BasketItemDto dto) => new(
        dto.Id,
        dto.BasketId,
        dto.ProductId,
        dto.Quantity);

    public static CreateBasketItemDto ToDto(this CreateBasketItemRequest request) => new(
        request.BasketId,
        request.ProductId,
        request.Quantity);
}