namespace ShopApi.Contracts.ProductImages;

public record CreateProductImageRequest(
    long ProductId,
    string ImageUrl);
