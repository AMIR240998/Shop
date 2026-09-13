using ShopApplication.DTOs.Basket;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class BasketService(IBasketRepository basketRepository)
{
    public async Task<IReadOnlyList<BasketItemDto>> GetItemsByIdAsync(long basketId,
        CancellationToken cancellationToken = default)
    {
        var basket = await basketRepository.GetBasketByIdAsync(basketId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Basket), basketId);

        var items = basket.Items.ToList();
        
        return items.Select(item => ToDto(item)).ToList();
    }

    public async Task<BasketItemDto?> GetItemByIdAsync(long basketId, long itemId,
        CancellationToken cancellationToken = default)
    {
        var basket = await basketRepository.GetBasketByIdAsync(basketId,cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Basket), basketId);
        
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId)
            ?? throw new EntityNotFoundException(nameof(BasketItem), itemId);
        
        return ToDto(item);
    }

    public async Task<BasketItemDto> AddItemAsync(CreateBasketItemDto dto, CancellationToken cancellationToken = default)
    {
        var basket = await basketRepository.GetBasketByIdAsync(dto.BasketId, cancellationToken)
            ??  throw new EntityNotFoundException(nameof(Basket), dto.BasketId);
        
        var item = new BasketItem(dto.BasketId, dto.ProductId, dto.Quantity);
        
        basket.AddItem(item);
        
        await basketRepository.SaveChangesAsync(cancellationToken);
        
        return ToDto(item);
    }

    public async Task RemoveItem(long basketId,long itemId, CancellationToken cancellationToken = default)
    {
        var basket = await basketRepository.GetBasketByIdAsync(basketId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Basket), basketId);
        
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId)
            ?? throw new EntityNotFoundException(nameof(BasketItem), itemId);
        
        basket.RemoveItem(item);
        
        await basketRepository.SaveChangesAsync(cancellationToken);
    }
    
    private static BasketItemDto ToDto(BasketItem basketItem)=> new(
        basketItem.Id,
        basketItem.BasketId,
        basketItem.ProductId,
        basketItem.Quantity);
}