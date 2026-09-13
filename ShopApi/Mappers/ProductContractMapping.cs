using ShopApi.Contracts.Products;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Product;

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
        dto.ContentType,
        dto.CategoryId,
        dto.IsActive,
        dto.IsDeleted,
        dto.CreatedAt,
        dto.UpdatedAt);

    public static CreateProductDto MapToDtoCreate(this CreateProductRequest request)
    { 
        var stream = request.ImageUrl.OpenReadStream();

        var dto = new CreateProductDto(
            Name: request.Name,
            Description: request.Description,
            Price: request.Price,
            Stock: request.Stock,
            new FileUpload(
                stream,
                request.ImageUrl.FileName,
                request.ImageUrl.ContentType),
            request.CategoryId);
        
        return dto;
    }



    public static UpdateProductDto MapToDtoUpdate(this UpdateProductRequest request)
    {
        var stream = request.ImageUrl.OpenReadStream();

        var dto = new UpdateProductDto(
            Name: request.Name,
            Description: request.Description,
            Price: request.Price,
            Stock: request.Stock,
            new FileUpload(
                stream,
                request.ImageUrl.FileName,
                request.ImageUrl.ContentType),
            request.CategoryId);
        
        return dto;
    }
        
    

    public static FilterProductDto MapToFilterDto(this FilterProductResponse response) => new(
        response.SearchPhase,
        response.CategoryId,
        response.MaxPrice);
}