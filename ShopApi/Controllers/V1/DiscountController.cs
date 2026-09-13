using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Discount;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;
[ApiVersion(1.0)]
public class DiscountController(DiscountService service) : BaseController
{
    [HttpGet(DiscountUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<DiscountResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var discount = await service.GetAllAsync(cancellationToken);
        return discount.Select(d => d.ToResponse()).ToList();
    }

    [HttpGet(DiscountUrlConstant.GetById)]
    public async Task<ApiResult<DiscountResponse>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var discount = await service.GetByIdAsync(id, cancellationToken);
        return discount.ToResponse();
    }

    [HttpPost(DiscountUrlConstant.Add)]
    public async Task<ApiResult<DiscountResponse>> Add(CreateDiscountRequest request,
        CancellationToken cancellationToken = default)
    {
        var discount = await service.AddAsync(request.CreateToDto(), cancellationToken);
        return discount.ToResponse();
    }

    [HttpDelete(DiscountUrlConstant.Remove)]
    public async Task<ApiResult> Remove(long id, CancellationToken cancellationToken = default)
    {
        await service.Remove(id, cancellationToken);
        return ApiResult.Secceded();
    }
}