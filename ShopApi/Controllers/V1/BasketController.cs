using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Basket;
using ShopApi.Mappers;
using ShopApplication.Services;
using ShopApplication.Services.Implementation;
using ShopApplication.Services.Interfaces;

namespace ShopApi.Controllers.V1;
[ApiVersion(1.0)]
public class BasketController(IBasketService basketService) : BaseController
{
    [HttpGet(BasketUrlConstant.GetItemsById)]
    public async Task<ApiResult<IReadOnlyList<BasketItemResponse>>> GetItemsById(long basketId,
        CancellationToken cancellationToken = default)
    {
        var items = await basketService.GetItemsByIdAsync(basketId, cancellationToken);
        return items.Select(i => i.ToResponse()).ToList();
    }

    [HttpGet(BasketUrlConstant.GetItemById)]
    public async Task<ApiResult<BasketItemResponse>> GetItemById(long basketId, long itemId,
        CancellationToken cancellationToken = default)
    {
        var item = await  basketService.GetItemByIdAsync(basketId, itemId, cancellationToken);
        return item.ToResponse();
    }

    [HttpPost(BasketUrlConstant.AddItem)]
    public async Task<ApiResult<BasketItemResponse>> AddItem([FromBody] CreateBasketItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await basketService.AddItemAsync(request.ToDto(), cancellationToken);
        return item.ToResponse();
    }

    [HttpDelete(BasketUrlConstant.RemoveItem)]
    public async Task<ApiResult> RemoveItem(long basketId, long itemId,
        CancellationToken cancellationToken = default)
    {
        await basketService.RemoveItem(basketId, itemId, cancellationToken);
        return ApiResult.Secceded();
    }
}