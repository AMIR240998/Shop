using ShopDomain.Exceptions;

namespace ShopDomain.Entities;

public class BasketItem
{
    public long Id { get; private set; }
    
    public long BasketId { get; private set; }
    
    public long ProductId { get; private set; }
    
    public int Quantity { get; private set; }
    
    public Product Product { get; private set; }

    private BasketItem()
    {
    }

    public BasketItem(long basketId, long productId, int quantity)
    {
        if(quantity <= 0)
            throw new InvalidQuantityException(nameof(quantity));
        
        BasketId = basketId;
        ProductId = productId;
        Quantity = quantity;
    }
}