namespace ShopApi.Contracts.ProductImages;

public record ProductImageResponse(
    long Id,
    long ProductId,
    string ImageUrl);