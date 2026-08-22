namespace ShopApi.Contracts.Products;

public record FilterProductsResponse(
    string? SearchPhase,
    long? CategoryId,
    long? MaxPrice);
