namespace ShopApplication.DTOs;

public record ExceptionLogDto(
    string? Id,
    string Message,
    string? ExceptionType,
    string? StackTrace,
    string? RequestPath,
    string? HttpMethod,
    int StatusCode,
    DateTimeOffset CreatedAt);