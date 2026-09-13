using ShopApi.Contracts.ExceptionLog;
using ShopApplication.DTOs;

namespace ShopApi.Mappers;

public static class ExceptionLogContractMapping
{
    public static ExceptionLogResponse ToResponse(this ExceptionLogDto dto) => new(
        dto.Id,
        dto.Message,
        dto.ExceptionType,
        dto.StackTrace,
        dto.RequestPath,
        dto.HttpMethod,
        dto.StatusCode,
        dto.CreatedAt);
}