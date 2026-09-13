namespace ShopDomain.Exceptions;

public class ImageNotFoundException(string imageUrl) : 
    Exception($"image {imageUrl} not found")
{
}