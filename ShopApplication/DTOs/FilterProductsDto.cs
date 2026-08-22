namespace ShopApplication.DTOs;

public record FilterProductsDto(
    string? SearchPhase,
    long? CategoryId,
    long? MaxPrice);