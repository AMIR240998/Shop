using System.Data;
using System.Net;

namespace ShopDomain.Entities;

public class ProductImages
{
    public long Id { get;private set; }

    public long ProductId { get;private set; }
    
    public string ImageUrl { get;private set; }


    public ProductImages()
    {
    }

    public ProductImages( long productId, string imageUrl)
    {
        ProductId = productId;
        ImageUrl = imageUrl;
    }
    
    public void Update(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}