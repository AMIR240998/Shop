namespace ShopDomain.Entities;

public class Comments
{
    public long Id { get;private set; }

    public long CustomerId { get;private set; }
    
    public long ProductId { get;private set; }

    public string Text { get;private set; }

    public short Rate { get;private set; }
    
    public DateTimeOffset CreatedAt { get;private set; }

    private Comments()
    {
    }

    public Comments(long id, long customerId, long productId, string text, short rate)
    {
        Id = id;
        CustomerId = customerId;
        ProductId = productId;
        Text = text;
        Rate = rate;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}