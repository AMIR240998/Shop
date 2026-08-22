namespace ShopApplication.DTOs;

public record ProductImageDto(
    long Id,
    long ProductId,
    string ImageUrl);