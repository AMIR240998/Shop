namespace ShopApplication.DTOs.Comment;

public record CreateCommentDto(
    long UserId,
    long ProductId,
    string Text,
    short Rate);