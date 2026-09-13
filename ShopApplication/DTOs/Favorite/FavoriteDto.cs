namespace ShopApplication.DTOs.Favorite;

public record FavoriteDto(
    long Id,
    long UserId,
    long ProductId);