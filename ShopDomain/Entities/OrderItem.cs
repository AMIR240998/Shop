namespace ShopDomain.Entities;

public class OrderItem
{
    public long Id { get;private set; }

    public long OrderId { get; private set; } 

    public long ProductId { get;private set; }

    public int Count { get;private set; }

    public decimal Price { get;private set; }
    
    
    private OrderItem()
    {
    }

    public OrderItem(long productId, int count, decimal price)
    {
        ProductId = productId;
        Count = count;
        Price = price;
    }
}