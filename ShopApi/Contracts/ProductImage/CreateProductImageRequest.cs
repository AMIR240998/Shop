namespace ShopApi.Contracts.ProductImage;

public record CreateProductImageRequest(
    long ProductId,
    IFormFile ImageUrl);
