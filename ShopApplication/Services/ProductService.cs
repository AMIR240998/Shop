using System.Net.Mime;
using ShopApplication.Caching;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Product;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class ProductService(IProductRepository productRepository,
    ProductCache productCache,
    IFileStorage arvanCloudFileStorage)
{
    public Task<PagedResultDto<ProductDto>> GetAllPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        => productCache.GetAllPagedAsync(
            async ct =>
            {
                var result = await productRepository.GetAllPagedAsync(
                    pageNumber,
                    pageSize,
                    ct);

                return (
                    result.productsList.Select(ToDto).ToList(),
                    result.count
                );
            },
            pageNumber,
            pageSize,
            cancellationToken);


    public Task<IReadOnlyList<ProductDto>> GetAllAsync(FilterProductDto dto,CancellationToken cancellationToken = default)
            => productCache.GetAllAsync(async ct => (await productRepository.GetAllAsync(dto.SearchPhase,dto.CategoryId,dto.MaxPrice,ct)).Select(ToDto).ToList(),
            cancellationToken);


    public async Task<ProductDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var productDto = await productCache.GetByIdAsync(id,async ct => ToDto(await productRepository.GetByIdAsync(id,ct) 
               ?? throw new EntityNotFoundException(nameof(Product),ct)), cancellationToken);
        return productDto;
    }

    public async Task<(Stream? stream, string contentType)> GetImageById(long id, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id,cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product),id);

        var image = await arvanCloudFileStorage.GetAsync(product.ImageUrl, cancellationToken)
                    ?? throw new ImageNotFoundException(nameof(product.ImageUrl));
        
        return (image, product.ContentTypeImage);
    }
     

    public async Task<ProductDto> AddAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var imageKey = await UploadImage(dto.ImageUrl,cancellationToken);
        
        var product = new Product(0,dto.Name,dto.Description,dto.Price,dto.Stock,imageKey,
            dto.ImageUrl.ContentType,dto.CategoryId);
        
        await productRepository.AddAsync(product, cancellationToken);
        await productRepository.SaveChangesAsync(cancellationToken);
        
        await productCache.InvalidateAsync(product.Id,cancellationToken);
        
        return ToDto(product);
    }

    public async Task<ProductDto> UpdateAsync(long id,UpdateProductDto updateProductDto, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product),id);
        
        await arvanCloudFileStorage.DeleteAsync(product.ImageUrl, cancellationToken);
        
        var imageKey = await UploadImage(updateProductDto.ImageUrl,cancellationToken);
        
        product.Update(updateProductDto.Name,updateProductDto.Description,updateProductDto.Price,
            updateProductDto.Stock,imageKey,updateProductDto.CategoryId);
        
        productRepository.Update(product);
        await productRepository.SaveChangesAsync(cancellationToken);
        
        await productCache.InvalidateAsync(product.Id,cancellationToken);
        
        
        return ToDto(product);
    }

    public async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product),id);

        await arvanCloudFileStorage.DeleteAsync(product.ImageUrl, cancellationToken);
        
        productRepository.Remove(product);
        await productRepository.SaveChangesAsync(cancellationToken);
        
        await productCache.InvalidateAsync(product.Id,cancellationToken);
        
    }

    public async Task<IReadOnlyList<ProductDto>> UpdatePriceAsync(decimal percent, int categoryId,
        CancellationToken cancellationToken = default)
    {
        var products = await productRepository.UpdatePriceAsync(percent, categoryId, cancellationToken);
        
        await productRepository.SaveChangesAsync(cancellationToken);
        return products.Select(ToDto).ToList();
    }
    
    private static ProductDto ToDto(Product product) => new (
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.Stock,
        product.ImageUrl,
        product.ContentTypeImage,
        product.CategoryId,
        product.IsActive, 
        product.IsDelete,
        product.CreatedAt,
        product.UpdatedAt);
    
    
    
    public async Task<ProductImageDto> GetByIdImageAsync(long productId,long productImageId,
        CancellationToken cancellationToken)
    {
        var product = await  productRepository.GetByIdAsync(productId,cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product),productId);
        
        var image = product.ProductImages.FirstOrDefault(image => image.ProductId == productImageId)
            ?? throw new EntityNotFoundException(nameof(ProductImage), productImageId);
        
        return MapToDto(image);
    }
    
    
    public async Task<ProductImageDto> AddImageAsync(CreateProductImageDto dto,CancellationToken cancellationToken)
    {
        var product = await  productRepository.GetByIdAsync(dto.ProductId,cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product),dto.ProductId);

        var image = new ProductImage(dto.ProductId, dto.ImageUrl.FileName,dto.ImageUrl.ContentType);
        product.AddImage(image);
        
        await productRepository.SaveChangesAsync(cancellationToken);
        
        return  MapToDto(image);
    }

    public async Task<ProductImageDto> UpdateImageAsync(UpdateProductImageDto dto,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(dto.ProductId, cancellationToken)
                    ?? throw new EntityNotFoundException(nameof(ProductImage), dto.ProductId);
        
        var image = product.ProductImages.FirstOrDefault(image => image.Id == dto.ImageId)
            ?? throw new EntityNotFoundException(nameof(ProductImage), dto.ImageId);
        
        var imageUploaded = await UpdateImage(dto.Image,cancellationToken);
        
        image.Update(imageUploaded);
        
        await productRepository.SaveChangesAsync(cancellationToken);
        
        return MapToDto(image);
    }

    public async Task RemoveImageAsync(long productId,long imageId, CancellationToken cancellationToken)
    {
        var product = await  productRepository.GetByIdAsync(productId,cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product),productId);
        
        var image = product.ProductImages.FirstOrDefault(image => image.Id == imageId)
            ?? throw new EntityNotFoundException(nameof(ProductImage), imageId);
        
        product.RemoveImage(image);
        
        await productRepository.SaveChangesAsync(cancellationToken);  
    }

    private async Task<string> UploadImage(FileUpload image,CancellationToken cancellationToken)
    {
        await using var stream = image.Content;
        var imageUploaded = await arvanCloudFileStorage.GetAsync(image.FileName, cancellationToken);
        
        if (imageUploaded is not null)
            throw new DuplicateImageException(image.FileName);
        
        var imageKey = await arvanCloudFileStorage.UploadAsync(
            stream,
            image.FileName,
            image.ContentType,
            cancellationToken);
        return imageKey;
    }

    private async Task<string> UpdateImage(FileUpload image, CancellationToken cancellationToken = default)
    {
        var imageUploaded = await arvanCloudFileStorage.GetAsync(image.FileName, cancellationToken);

        if(imageUploaded is not null)
        {
           await arvanCloudFileStorage.DeleteAsync(image.FileName, cancellationToken);
        }
        return await arvanCloudFileStorage.UploadAsync(image.Content,image.FileName,
            image.ContentType,cancellationToken);
        
    }
    private static ProductImageDto MapToDto(ProductImage image) => new(
        image.Id,
        image.ProductId,
        image.ImageUrl);
}