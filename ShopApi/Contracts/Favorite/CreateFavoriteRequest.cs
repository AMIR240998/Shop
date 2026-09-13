namespace ShopApi.Contracts.Favorite;

public record CreateFavoriteRequest(
    long UserId,
    long ProductId);