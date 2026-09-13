namespace ShopApplication.DTOs.Product;

public record CreateProductImageDto(
    long ProductId,
    FileUpload ImageUrl);
