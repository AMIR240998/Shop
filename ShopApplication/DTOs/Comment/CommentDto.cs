namespace ShopApplication.DTOs.Comment;

public record CommentDto(
    string? Id,
    long UserId,
    long ProductId,
    string Text,
    short Rate,
    DateTimeOffset CreatedAt);