using ShopApplication.DTOs.Basket;

namespace ShopApplication.Services.Interfaces;

public interface IBasketService
{
    Task<IReadOnlyList<BasketItemDto>> GetItemsByIdAsync(long basketId,
        CancellationToken cancellationToken = default);


    Task<BasketItemDto?> GetItemByIdAsync(long basketId, long itemId,
        CancellationToken cancellationToken = default);


    Task<BasketItemDto> AddItemAsync(CreateBasketItemDto dto, CancellationToken cancellationToken = default);


    Task RemoveItem(long basketId, long itemId, CancellationToken cancellationToken = default);
}