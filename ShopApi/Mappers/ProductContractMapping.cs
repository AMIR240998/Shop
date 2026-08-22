using ShopApi.Contracts.Products;
using ShopApplication.DTOs;

namespace ShopApi.Mappers;

public static class ProductContractMapping
{
    public static ProductResponse MapToResponse(this ProductDto dto) => new(
        dto.Id,
        dto.Name,
        dto.Description,
        dto.Price,
        dto.Stock,
        dto.ImageUrl,
        dto.CategoryId,
        dto.IsActive,
        dto.IsDeleted,
        dto.CreatedAt,
        dto.UpdatedAt);
    
    public static CreateProductDto MapToDtoCreate(this CreateProductRequest request) => new (
        request.Name,
        request.Description,
        request.Price,
        request.Stock,
        request.ImageUrl,
        request.CategoryId);
    
    
    public static UpdateProductDto MapToDtoUpdate(this UpdateProductRequest request) => new (
        request.Name,
        request.Description,
        request.Price,
        request.Stock,
        request.ImageUrl,
        request.CategoryId);
    

    public static FilterProductsDto MapToFilterDto(this FilterProductsResponse response) => new(
        response.SearchPhase,
        response.CategoryId,
        response.MaxPrice);
}