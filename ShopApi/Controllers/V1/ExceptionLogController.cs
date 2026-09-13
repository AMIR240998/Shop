using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.ExceptionLog;
using ShopApi.Mappers;
using ShopApplication.Services;
using ShopDomain.Entities;

namespace ShopApi.Controllers.V1;

public class ExceptionLogController(ExceptionLogService service) : BaseController
{
    [HttpGet(ExceptionLogUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<ExceptionLogResponse>>> GetAll(
        CancellationToken cancellationToken = default)
    {
        var exceptions = await service.GetAllAsync(cancellationToken);
        return exceptions.Select(e => e.ToResponse()).ToList();
    }

    [HttpGet(ExceptionLogUrlConstant.GetById)]
    public async Task<ApiResult<ExceptionLogResponse>> GetById(string id, CancellationToken cancellationToken = default)
    {
        var exception = await service.GetByIdAsync(id, cancellationToken);
        return exception.ToResponse();
    }

    [HttpGet(ExceptionLogUrlConstant.GetRecently)]
    public async Task<ApiResult<IReadOnlyList<ExceptionLogResponse>>> GetRecently(int count,
        CancellationToken cancellationToken = default)
    {
        var exceptions = await service.GetRecentlyAsync(count, cancellationToken);
        return exceptions.Select(e => e.ToResponse()).ToList();
    }

    [HttpDelete(ExceptionLogUrlConstant.Remove)]
    public async Task<ApiResult> RemoveAll(CancellationToken cancellationToken = default)
    {
        await service.RemoveAllAsync(cancellationToken);
        return ApiResult.Secceded();
    }
    
}