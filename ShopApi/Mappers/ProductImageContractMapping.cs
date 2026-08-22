using ShopApi.Contracts.ProductImages;
using ShopApplication.DTOs;

namespace ShopApi.Mappers;

public static class ProductImageContractMapping
{
    public static ProductImageResponse MapToResponse(this ProductImageDto dto) => new(
        dto.Id,
        dto.ProductId,
        dto.ImageUrl);
    
    public static CreateProductImageDto MapToDto(this CreateProductImageRequest request) => new (
        request.ProductId,
        request.ImageUrl);
}