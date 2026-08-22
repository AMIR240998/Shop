namespace ShopApi.Contracts.Products;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int? Stock,
    string ImageUrl,
    int CategoryId);
