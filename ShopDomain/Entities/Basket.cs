namespace ShopDomain.Entities;

public class Basket
{
    private readonly List<BasketItem> _items = [];

    public long Id { get; private set; }
    
    public long UserId { get;private set; }
    
    public DateTimeOffset CreateAt { get;private set; }

    public DateTimeOffset? UpdateAt { get; private set; }
    

    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
    
    public Basket()
    {
        CreateAt =  DateTimeOffset.UtcNow;
    }
    
    public void UpdateDateTime()
    {
        UpdateAt = DateTimeOffset.Now;
    }

    public void AddItem(BasketItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(BasketItem item)
    {
        _items.Remove(item);
    }

    public void ClearItems()
    {
        _items.Clear();
    }
}