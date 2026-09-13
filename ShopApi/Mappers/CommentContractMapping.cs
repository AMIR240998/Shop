using ShopApi.Contracts.Comment;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Comment;

namespace ShopApi.Mappers;

public static class CommentContractMapping
{
    public static CommentResponse ToResponse(this CommentDto dto) => new(
        dto.Id,
        dto.UserId,
        dto.ProductId,
        dto.Text,
        dto.Rate,
        dto.CreatedAt);

    public static CreateCommentDto ToCreateCommentDto(this CreateCommentRequest request) => new(
        request.UserId,
        request.ProductId,
        request.Text,
        request.Rate);
    
    public static UpdateCommentDto ToUpdateCommentDto(this UpdateCommentRequest request) => new(
        request.Text,
        request.Rate);
}