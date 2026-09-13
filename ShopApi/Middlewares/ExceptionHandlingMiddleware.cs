using System.ComponentModel.DataAnnotations;
using System.Net;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApi.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context,IExceptionLogRepository repository)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await AddExceptionLogAsync(context, ResolveStatusCode(ex), ex, repository);
            await WriteProblemAsync(context, ResolveStatusCode(ex), ex.Message);
        }
    }

    private static HttpStatusCode ResolveStatusCode(Exception ex)
    {
        return ex switch
        {
            DuplicateUserNameException
                or InsufficientStockException 
                or DuplicateImageException
                => HttpStatusCode.Conflict,

            EntityNotFoundException
                or NotExistUserNameException
                or ImageNotFoundException
                => HttpStatusCode.NotFound,

            ArgumentException
                or ValidationException
                or InvalidQuantityException
                or InvalidBalanceException
                or InvalidPriceException
                or ArgumentOutOfRangeException
                => HttpStatusCode.BadRequest,

            UnauthorizedAccessException
                => HttpStatusCode.Unauthorized,

            _ => HttpStatusCode.InternalServerError
        };
    }

    private async Task AddExceptionLogAsync(HttpContext context,HttpStatusCode statusCode,Exception ex, IExceptionLogRepository repository)
    {
        try
        {
            var log = ExceptionLog.AddLog(ex,context.Request.Path,context.Request.Method,(int)statusCode);
            await repository.AddAsync(log,context.RequestAborted);
        }
        catch (Exception exception)
        {

            logger.LogError(
                exception,
                "Failed to save exception log to MongoDB.");

            var status = ResolveStatusCode(exception);

            await WriteProblemAsync(
                context,
                status,
                ex.Message);
        }
    }
    
    private static Task WriteProblemAsync(HttpContext context,HttpStatusCode statusCode , string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new
        {
            status = (int)statusCode,
            title = statusCode.ToString(),
            message
        };
        return context.Response.WriteAsJsonAsync(problem);
    }
}