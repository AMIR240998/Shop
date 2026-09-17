using ShopApplication.DTOs;
using ShopApplication.DTOs.Product;

namespace ShopApplication.Services.Interface;

public interface IProductService
{
    public Task<PagedResultDto<ProductDto>> GetAllPagedAsync(int pageNumber, int pageSize, 
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<ProductDto>> GetAllAsync(FilterProductDto dto, CancellationToken cancellationToken = default);



     Task<ProductDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);


    Task<(Stream? stream, string contentType)> GetImageById(long id, CancellationToken cancellationToken = default);



    Task<ProductDto> AddAsync(CreateProductDto dto, CancellationToken cancellationToken = default);


    Task<ProductDto> UpdateAsync(long id, UpdateProductDto updateProductDto,
        CancellationToken cancellationToken = default);


    Task RemoveAsync(long id, CancellationToken cancellationToken = default);


    Task<IReadOnlyList<ProductDto>> UpdatePriceAsync(decimal percent, int categoryId,
        CancellationToken cancellationToken = default);
    
    
    Task<ProductImageDto> GetByIdImageAsync(long productId,long productImageId, CancellationToken cancellationToken);

    
    
    Task<ProductImageDto> AddImageAsync(CreateProductImageDto dto,CancellationToken cancellationToken);
  

    Task<ProductImageDto> UpdateImageAsync(UpdateProductImageDto dto, CancellationToken cancellationToken);
 

    Task RemoveImageAsync(long productId,long imageId, CancellationToken cancellationToken);

}