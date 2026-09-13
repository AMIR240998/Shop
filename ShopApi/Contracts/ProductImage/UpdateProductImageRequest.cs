namespace ShopApi.Contracts.ProductImage;

public record UpdateProductImageRequest(
    long ProductId,
    long ImageId,
    IFormFile Image);