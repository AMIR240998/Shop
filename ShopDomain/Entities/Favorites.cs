namespace ShopDomain.Entities;

public class Favorites
{
    public long Id { get;private set; }
    public long CustomerId { get;private set; }
    public long ProductId { get;private set; }
    
    public virtual Customers Customer { get; private init; }
    public virtual Products Product { get; private init; }

    private Favorites()
    {
    }

    public Favorites(long id, long customerId, long productId, Customers customer,  Products product)
    {
        Id = id;
        CustomerId = customerId;
        ProductId = productId;
        Customer = customer;
        Product = product;
    }
}