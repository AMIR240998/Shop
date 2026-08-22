using ShopApplication.Caching;
using ShopApplication.DTOs;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class ProductService(IProductRepository productRepository,ProductCache productCache)
{
    public Task<IReadOnlyList<ProductDto>> GetAllAsync(FilterProductsDto dto,CancellationToken cancellationToken = default)
        => productCache.GetAllAsync(async ct => (await productRepository.GetAllAsync(dto.SearchPhase,dto.CategoryId,dto.MaxPrice,ct)).Select(ToDto).ToList(),
            cancellationToken);
    
    
    public Task<ProductDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => productCache.GetByIdAsync(id,
            async ct =>ToDto(await productRepository.GetByIdAsync(id,ct)
                            ?? throw new EntityNotFoundException(nameof(Products),
                                ct)), cancellationToken);

    public async Task<ProductDto> AddAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = new Products(0,dto.Name,dto.Description,dto.Price,dto.Stock,dto.ImageUrl,dto.CategoryId);
        
        await productRepository.AddAsync(product, cancellationToken);
        await productRepository.SaveChangesAsync(cancellationToken);
        
        await productCache.InvalidateAsync(product.Id,cancellationToken);
        
        return ToDto(product);
    }

    public async Task<ProductDto> UpdateAsync(long id,UpdateProductDto updateProductDto, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Products),id);
        
        product.Update(updateProductDto.Name,updateProductDto.Description,updateProductDto.Price,
            updateProductDto.Stock,updateProductDto.ImageUrl,updateProductDto.CategoryId);
        
        productRepository.Update(product);
        await productRepository.SaveChangesAsync(cancellationToken);
        
        await productCache.InvalidateAsync(product.Id,cancellationToken);
        
        
        return ToDto(product);
    }

    public async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Products),id);
        
        productRepository.Remove(product);
        await productRepository.SaveChangesAsync(cancellationToken);
        
        await productCache.InvalidateAsync(product.Id,cancellationToken);
        
    }
    
    private static ProductDto ToDto(Products product) => new (
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.Stock,
        product.ImageUrl,
        product.CategoryId,
        product.IsActive, 
        product.IsDelete,
        product.CreatedAt,
        product.UpdatedAt);
}