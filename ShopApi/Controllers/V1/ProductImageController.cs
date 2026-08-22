using Microsoft.AspNetCore.Mvc;
using ShopApi.Contracts.ProductImages;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;

[ApiController]
[Route("api/[controller]")]
public class ProductImageController(ProductImageService productImageService) : ControllerBase
{
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductImageResponse>> GetById(long id, CancellationToken cancellationToken)
    {
        var productImage = await productImageService.GetByIdAsync(id, cancellationToken);
        return Ok(productImage.MapToResponse());
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductImageResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var productImage = await productImageService.GetAllAsync(cancellationToken);
        return Ok(productImage.Select(p => p.MapToResponse()).ToList());
    }
    [HttpPost]
    public async Task<ActionResult<ProductImageResponse>> Add(CreateProductImageRequest request, CancellationToken cancellationToken)
    {
        var image = await productImageService.AddAsync(request.MapToDto(), cancellationToken);
        return Ok(image.MapToResponse());
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ProductImageResponse>> Update(long id,string imageUrl, CancellationToken cancellationToken)
    {
        var image = await productImageService.UpdateAsync(id, imageUrl, cancellationToken);
        return image.MapToResponse();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Remove(long id, CancellationToken cancellationToken)
    {
        await productImageService.RemoveAsync(id, cancellationToken);
        return Ok();
    }
}