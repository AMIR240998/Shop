namespace ShopApi.Contracts.Products;

public record FilterProductResponse(
    string? SearchPhase,
    long? CategoryId,
    long? MaxPrice);
