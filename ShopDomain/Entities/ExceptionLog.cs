namespace ShopDomain.Entities;

public class ExceptionLog
{
    public string? Id { get;private set; } = null!;

    public string Message { get;private set; } = null!;

    public string? ExceptionType { get; private set; }

    public string? StackTrace { get;private set; }

    public string? RequestPath { get;private set; }

    public string? HttpMethod { get;private set; }

    public int StatusCode { get;private set; }

    public DateTimeOffset CreatedAt { get;private set; }

    private ExceptionLog()
    {
        
    }

    public static ExceptionLog AddLog(Exception ex, string requestPath, string httpMethod,int  statusCode)
    {
        return new ExceptionLog
        {
            Message = ex.Message,
            ExceptionType = ex.GetType().FullName,
            StackTrace = ex.StackTrace,
            RequestPath = requestPath,
            HttpMethod = httpMethod,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow,
        };

    }
}