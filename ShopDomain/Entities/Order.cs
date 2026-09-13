using ShopDomain.Enums;
using ShopDomain.Exceptions;

namespace ShopDomain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = [];
    public long Id { get;private set; }

    public long UserId { get;private set; } 

    public DateTimeOffset? OrderDate { get;private set; }

    public string Status { get;private set; }

    public decimal TotalPrice { get;private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly(); 


    private Order()
    {
    }
    

    public Order(long userId)
    {
        UserId = userId;
        OrderDate = DateTimeOffset.UtcNow;
        Status = nameof(OrderStatus.Pending);
    }
    
    public void ChangeStatus(OrderStatus status)
    {
        Status = status.ToString();
    }

    private void CalculateTotalPrice()
    {
        TotalPrice = _items.Sum(i => i.Price *  i.Count);
    }
    public void InsertItem(OrderItem item)
    {
        if(item.Count <= 0)
            throw new InvalidQuantityException(nameof(item));
        
        _items.Add(item);
        CalculateTotalPrice();
    }
}