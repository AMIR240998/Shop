namespace ShopApplication.DTOs;

public record FilterProductDto(
    string? SearchPhase,
    long? CategoryId,
    long? MaxPrice);