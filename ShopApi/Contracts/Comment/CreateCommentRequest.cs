namespace ShopApi.Contracts.Comment;

public record CreateCommentRequest(
    long UserId,
    long ProductId,
    string Text,
    short  Rate);