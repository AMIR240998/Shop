namespace ShopApi.Contracts.Products;

public record ProductResponse(
    long Id,
    string Name,
    string? Description,
    decimal Price,
    int? Stock,
    string ImageUrl,
    string? ContentType,
    int CategoryId,
    bool IsActive,
    bool IsDeleted,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
    );