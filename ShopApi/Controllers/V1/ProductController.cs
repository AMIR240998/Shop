using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Pagination;
using ShopApi.Contracts.ProductImage;
using ShopApi.Contracts.ProductImages;
using ShopApi.Contracts.Products;
using ShopApi.Mappers;
using ShopApplication.Services;
using ShopApplication.Services.Implementation;
using ShopApplication.Services.Interface;

namespace ShopApi.Controllers.V1;

[ApiVersion(1.0)]
public class ProductController(IProductService productService) : BaseController
{
    [HttpGet(ProductUrlConstant.GetAllPaged)]
    public async Task<ApiResult<PagedResult<ProductResponse>>> GetAllPaged([FromQuery] PaginationRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await productService.GetAllPagedAsync(request.PageNumber,
            request.PageSize, cancellationToken);
            
        var response = new PagedResult<ProductResponse>
        {
            Items = product.Items.Select(p => p.MapToResponse()).ToList(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = product.TotalCount
        };
        return ApiResult<PagedResult<ProductResponse>>.Secceded(response);
    }
    
    
    [HttpGet(ProductUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<ProductResponse>>> GetAll([FromQuery] FilterProductResponse response,CancellationToken cancellationToken =  default)
    {
        var products  = await productService.GetAllAsync(response.MapToFilterDto(),cancellationToken);
        return products.Select(p => p.MapToResponse()).ToList();
    }
    
    [HttpGet(ProductUrlConstant.GetById)]
    public async Task<ApiResult<ProductResponse>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var dto = await productService.GetByIdAsync(id, cancellationToken);
        return dto.MapToResponse();
    }
    
    [HttpGet(ProductUrlConstant.GetImageProduct)]
    public async Task<IActionResult> GetImageProdut(long id,CancellationToken cancellationToken = default)
    {
        var (image,contentType) = await productService.GetImageById(id, cancellationToken);
        return File(image, contentType);
    }

    [HttpPost(ProductUrlConstant.Add)]
    public async Task<ApiResult<ProductResponse>> Add([FromForm] CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await productService.AddAsync(request.MapToDtoCreate(), cancellationToken);
        return product.MapToResponse();
    }

    [HttpPut(ProductUrlConstant.Update)]
    public async Task<ApiResult<ProductResponse>> Update(long id, [FromForm]UpdateProductRequest request,CancellationToken cancellationToken = default)
    {
        var product = await productService.UpdateAsync(id, request.MapToDtoUpdate(), cancellationToken);
        return product.MapToResponse();
    }

    [HttpPut(ProductUrlConstant.UpdatePrices)]
    public async Task<ApiResult<IReadOnlyList<ProductResponse>>> UpdatePrices(decimal percent, int categoryId,
        CancellationToken cancellationToken = default)
    {
        var products = await productService.UpdatePriceAsync(percent, categoryId, cancellationToken);
        return products.Select(product => product.MapToResponse()).ToList();
    }

    [HttpDelete(ProductUrlConstant.Remove)]
    public async Task<ApiResult> Remove(long id,CancellationToken cancellationToken = default)
    {
        await productService.RemoveAsync(id, cancellationToken);
        return ApiResult.NoContent();
    }
    
    
    
    
    [HttpGet(ProductImageUrlConstant.GetById)]
    public async Task<ApiResult<ProductImageResponse>> GetByIdImage(long productId,long imageId
        ,CancellationToken cancellationToken)
    {
        var productImage = await productService.GetByIdImageAsync(productId,imageId,cancellationToken);
        return productImage.MapToResponse();
    }
    
    [HttpPost(ProductImageUrlConstant.Add)]
    public async Task<ApiResult<ProductImageResponse>> AddImage([FromForm]CreateProductImageRequest request, CancellationToken cancellationToken)
    {
        var image = await productService.AddImageAsync(request.MapToDto(), cancellationToken);
        return image.MapToResponse();
    }

    [HttpPut(ProductImageUrlConstant.Update)]
    public async Task<ApiResult<ProductImageResponse>> UpdateImage(UpdateProductImageRequest request,
        CancellationToken cancellationToken)
    {
        var image = await productService.UpdateImageAsync(request.UpdateMapToDto(), cancellationToken);
        return image.MapToResponse();
    }

    [HttpDelete(ProductImageUrlConstant.Remove)]
    public async Task<ApiResult> RemoveImage(long productId,long imageId, CancellationToken cancellationToken)
    {
        await productService.RemoveImageAsync(productId, imageId,cancellationToken);
        return ApiResult.Secceded();
    }
    
}