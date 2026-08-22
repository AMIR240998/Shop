namespace ShopApplication.DTOs;

public record CreateProductImageDto(
    long ProductId,
    string ImageUrl);