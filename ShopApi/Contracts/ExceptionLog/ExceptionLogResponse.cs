namespace ShopApi.Contracts.ExceptionLog;

public record ExceptionLogResponse(
    string? Id,
    string Message,
    string? ExceptionType,
    string? StackTrace,
    string? RequestPath,
    string? HttpMethod,
    int StatusCode,
    DateTimeOffset CreatedAt);