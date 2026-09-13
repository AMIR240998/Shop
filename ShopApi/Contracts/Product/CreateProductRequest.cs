namespace ShopApi.Contracts.Products;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int? Stock,
    IFormFile ImageUrl,
    int CategoryId);
