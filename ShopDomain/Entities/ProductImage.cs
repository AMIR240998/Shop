using System.Data;
using System.Net;

namespace ShopDomain.Entities;

public class ProductImage
{
    public long Id { get;private set; }

    public long ProductId { get;private set; }
    
    public string ImageUrl { get;private set; }
    
    public string? ContentTypeImage { get;private set; }


    private ProductImage()
    {
    }

    public ProductImage(long productId, string imageUrl, string? contentTypeImage)
    {
        ProductId = productId;
        ImageUrl = imageUrl;
        ContentTypeImage = contentTypeImage;
    }
    
    public void Update(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}