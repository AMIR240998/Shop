using ShopApi.Contracts.Favorite;
using ShopApplication.DTOs.Favorite;

namespace ShopApi.Mappers;

public static class FavoriteContractMapping
{
    public static FavoriteResponse ToResponse(this FavoriteDto dto) => new(
        dto.Id,
        dto.UserId,
        dto.ProductId);
    
    public static CreateFavoriteDto ToDto(this CreateFavoriteRequest request) => new(
        request.UserId,
        request.ProductId);
}