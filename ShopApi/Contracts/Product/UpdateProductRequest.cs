namespace ShopApi.Contracts.Products;

public record UpdateProductRequest(    
    string Name,
    string Description,
    decimal Price,
    int? Stock,
    IFormFile ImageUrl,
    int CategoryId);