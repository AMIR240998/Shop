using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopApi.Contracts;

namespace ShopApi.Filters;

public class ApiResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ApiResult or ApiResult<object>)
        {
            await next();
            return;
        }

        int statusCode;
        switch (context.Result)
        {
            case OkObjectResult objectResult:
                statusCode = StatusCodes.Status200OK;
                WrapObjectResult(objectResult, statusCode);
                break;
            case CreatedResult createdResult:
                statusCode = StatusCodes.Status201Created;
                WrapObjectResult(createdResult, statusCode);
                break;
            case AcceptedResult acceptedResult:
                statusCode = StatusCodes.Status202Accepted;
                WrapObjectResult(acceptedResult, statusCode);
                break;
            case NotFoundResult:
                statusCode = StatusCodes.Status404NotFound;
                context.Result = new ObjectResult(ApiResult.Failed(context.Result?.ToString(), statusCode))
                {
                    StatusCode = statusCode
                };
                break;
            case NoContentResult:
                statusCode = StatusCodes.Status204NoContent;
                context.Result = new ObjectResult(ApiResult.NoContent())
                {
                    StatusCode = statusCode
                };
                break;
            case ObjectResult {StatusCode : not null} objectResult:
                statusCode = objectResult.StatusCode.Value;
                WrapObjectResult(objectResult, statusCode);
                break;
        }

        await next();
    }

    private static void WrapObjectResult(ObjectResult objectResult ,int  statusCode)
    {
        var apiResult = statusCode >= 200 && statusCode < 300
          ?ApiResult<object?>.Secceded(objectResult.Value, statusCode)
          : ApiResult.Failed(objectResult.Value?.ToString(),statusCode);

        objectResult.Value = apiResult;
        objectResult.StatusCode = statusCode;
    }
}