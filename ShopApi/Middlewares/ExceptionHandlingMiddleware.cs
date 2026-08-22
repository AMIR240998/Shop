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
            await WriteProblemAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
    }

    private static HttpStatusCode ResolveStatusCode(Exception ex)
    {
        return ex switch
        {
            DuplicateUserNameException => HttpStatusCode.Conflict,
            EntityNotFoundException or NotExistUserNameException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.BadRequest
        };
    }

    private async Task AddExceptionLogAsync(HttpContext context,HttpStatusCode statusCode,Exception ex, IExceptionLogRepository repository)
    {
        try
        {
            var log = ExceptionLogs.AddLog(ex,context.Request.Path,context.Request.Method,(int)statusCode);
            await repository.AddAsync(log,context.RequestAborted);
        }
        catch (Exception exeption)
        {
            logger.LogError(
                ex,
                "Unhandled exception: {ExceptionType}",
                ex.GetType().Name);

            var status = ResolveStatusCode(exeption);

            await AddExceptionLogAsync(
                context,
                status,
                ex,
                repository);

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