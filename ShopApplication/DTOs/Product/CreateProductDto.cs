namespace ShopApplication.DTOs.Product;

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int? Stock,
    FileUpload ImageUrl,
    int CategoryId
);
