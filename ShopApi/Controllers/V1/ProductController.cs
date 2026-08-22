using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Products;
using ShopApi.Mappers;
using ShopApplication.Services;
namespace ShopApi.Controllers.V1;

[ApiVersion(1.0)]
public class ProductController(ProductService productService) : BaseController
{
    [HttpGet(ProductsUrlConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<ProductResponse>>> GetAll([FromQuery] FilterProductsResponse response,CancellationToken cancellationToken =  default)
    {
        var products  = await productService.GetAllAsync(response.MapToFilterDto(),cancellationToken);
        return products.Select(p => p.MapToResponse()).ToList();
    }
    [HttpGet(ProductsUrlConstants.GetById)]
    public async Task<ApiResult<ProductResponse>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var product = await productService.GetByIdAsync(id, cancellationToken);
        return product.MapToResponse();
    }

    [HttpPost(ProductsUrlConstants.Add)]
    public async Task<ApiResult<ProductResponse>> Add(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await productService.AddAsync(request.MapToDtoCreate(), cancellationToken);
        return product.MapToResponse();
    }

    [HttpPut(ProductsUrlConstants.Update)]
    public async Task<ApiResult<ProductResponse>> Update(long id, UpdateProductRequest request,CancellationToken cancellationToken = default)
    {
        var product = await productService.UpdateAsync(id, request.MapToDtoUpdate(), cancellationToken);
        return product.MapToResponse();
    }

    [HttpDelete(ProductsUrlConstants.Remove)]
    public async Task<ApiResult> Remove(long id,CancellationToken cancellationToken = default)
    {
        await productService.RemoveAsync(id, cancellationToken);
        return ApiResult.NoContent();
    }
    
}