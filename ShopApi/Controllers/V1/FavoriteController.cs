using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Favorite;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;

public class FavoriteController(FavoriteService service) : BaseController
{
    [HttpGet(FavoriteUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<FavoriteResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var favorites = await service.GetAllAsync(cancellationToken);
        return favorites.Select(f => f.ToResponse()).ToList();
    }

    [HttpGet(FavoriteUrlConstant.GetById)]
    public async Task<ApiResult<FavoriteResponse>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var favorite = await service.GetByIdAsync(id, cancellationToken);
        return favorite.ToResponse();
    }

    [HttpPost(FavoriteUrlConstant.Add)]
    public async Task<ApiResult> Add(CreateFavoriteRequest request, CancellationToken cancellationToken = default)
    {
        await service.AddAsync(request.ToDto(), cancellationToken);
        return ApiResult.Secceded();
    }

    [HttpDelete(FavoriteUrlConstant.Remove)]
    public async Task<ApiResult> Remove(long id, CancellationToken cancellationToken = default)
    {
        await service.RemoveAsync(id, cancellationToken);
        return ApiResult.Secceded();
    }
}