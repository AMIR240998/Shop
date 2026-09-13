namespace ShopApplication.DTOs.Favorite;

public record CreateFavoriteDto(
    long UserId,
    long ProductId);