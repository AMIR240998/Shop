namespace ShopApi.Contracts.Favorite;

public record FavoriteResponse(
    long Id,
    long UserId,
    long ProductId);