namespace ShopApplication.DTOs.Product;

public record UpdateProductDto(
    string Name,
    string Description,
    decimal Price,
    int? Stock,
    FileUpload ImageUrl,
    int CategoryId);
