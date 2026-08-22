using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class Orders
{
    private readonly List<OrderItems> _items = [];
    public long Id { get;private set; }

    public long CustomerId { get;private set; } 

    public DateTimeOffset? OrderDate { get;private set; }

    public OrderStatus Status { get;private set; }

    public decimal TotalPrice { get;private set; }

    public IReadOnlyCollection<OrderItems> Items => _items.AsReadOnly(); 


    private Orders()
    {
    }
    

    public Orders(long id, long customerId, OrderStatus status, decimal totalPrice)
    {
        Id = id;
        CustomerId = customerId;
        OrderDate = DateTimeOffset.UtcNow;
        Status = status;
        TotalPrice = totalPrice;
    }

    public void InsertItem(OrderItems item)
    {
        _items.Add(item);
    }
}