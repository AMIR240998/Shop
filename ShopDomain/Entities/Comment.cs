namespace ShopDomain.Entities;

public class Comment
{
    public string? Id { get; private set; } = null!;

    public long UserId { get;private set; }
    
    public long ProductId { get;private set; }

    public string Text { get;private set; }

    public short Rate { get;private set; }
    
    public DateTimeOffset CreatedAt { get;private set; }

    private Comment()
    {
    }

    public Comment(long userId, long productId, string text, short rate)
    {
        UserId = userId;
        ProductId = productId;
        Text = text;
        Rate = rate;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    public void Update(string text, short rate)
    {
        Text = text;
        Rate = rate;
    }
}