using ShopApi.Contracts.ProductImage;
using ShopApi.Contracts.ProductImages;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Product;

namespace ShopApi.Mappers;

public static class ProductImageContractMapping
{
    public static ProductImageResponse MapToResponse(this ProductImageDto dto) => new(
        dto.Id,
        dto.ProductId,
        dto.ImageUrl);

    public static CreateProductImageDto MapToDto(this CreateProductImageRequest request)
    {
        var stream = request.ImageUrl.OpenReadStream();

        var dto = new CreateProductImageDto(
            ProductId: request.ProductId,
            new FileUpload(
                stream,
                request.ImageUrl.FileName,
                request.ImageUrl.ContentType));
        return dto;
    }

    public static UpdateProductImageDto UpdateMapToDto(this UpdateProductImageRequest request)
    {
        var stream = request.Image.OpenReadStream();
        var dto = new UpdateProductImageDto(
            ProductId: request.ProductId,
            ImageId: request.ImageId,
            new FileUpload(
                stream,
                request.Image.FileName,
                request.Image.ContentType));
        return dto;
    }
}