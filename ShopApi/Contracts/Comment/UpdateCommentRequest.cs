namespace ShopApi.Contracts.Comment;

public record UpdateCommentRequest(
    string Text,
    short Rate);