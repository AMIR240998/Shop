namespace ShopDomain.Entities;

public class OrderItems
{
    public long Id { get;private set; }

    public long OrderId { get;private set; }

    public long ProductId { get;private set; }

    public int Count { get;private set; }

    public decimal Price { get;private set; }
    
    
    private OrderItems()
    {
    }

    public OrderItems(long id, long orderId, long productId, int count, decimal price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Count = count;
        Price = price;
    }
}