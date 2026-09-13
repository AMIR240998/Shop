namespace ShopApi.Contracts.Comment;

public record CommentResponse(
    string? Id,
    long UserId,
    long ProductId,
    string Text,
    short Rate,
    DateTimeOffset CreatedAt);