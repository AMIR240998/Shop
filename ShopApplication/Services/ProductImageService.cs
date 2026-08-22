using ShopApplication.DTOs;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class ProductImageService(IProductImageRepository repository)
{
    public async Task<ProductImageDto> GetByIdAsync(long productImageId, CancellationToken cancellationToken)
    {
        var image = await repository.GetByIdAsync(productImageId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(ProductImages), productImageId);
        return MapToDto(image);
    }
    
    public async Task<IReadOnlyList<ProductImageDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var productImages = await repository.GetAllAsync(cancellationToken);
        return productImages.Select(MapToDto).ToList();
    }
    
    public async Task<ProductImageDto> AddAsync(CreateProductImageDto dto,CancellationToken cancellationToken)
    {
        var productImage = new ProductImages(dto.ProductId, dto.ImageUrl);
        await repository.AddAsync(productImage, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return  MapToDto(productImage);
    }

    public async Task<ProductImageDto> UpdateAsync(long id,string imageUrl,CancellationToken cancellationToken)
    {
        var image = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(ProductImages), id);
        
        image.Update(imageUrl); 
        repository.Update(image);
        await repository.SaveChangesAsync(cancellationToken);
        return MapToDto(image);
    }

    public async Task RemoveAsync(long productImageId, CancellationToken cancellationToken)
    {
        var image = await repository.GetByIdAsync(productImageId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(ProductImages), productImageId);
        
        repository.Remove(image);
        await repository.SaveChangesAsync(cancellationToken);
    }

    private static ProductImageDto MapToDto(ProductImages images) => new(
        images.Id,
        images.ProductId,
        images.ImageUrl);
}