namespace ShopDomain.Entities;

public class Favorite
{
    public long Id { get; private set; }
    public long UserId { get;private set; }
    public long ProductId { get;private set; }

    public virtual User User { get; private set; } = null!;
    public virtual Product Product { get; private set; } = null!;

    private Favorite()
    {
    }

    public Favorite(long userId, long productId)
    {
        UserId = userId;
        ProductId = productId;
    }
}