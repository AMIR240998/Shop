namespace ShopApplication.DTOs;

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int? Stock,
    string ImageUrl,
    int CategoryId
);
