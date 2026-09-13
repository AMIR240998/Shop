namespace ShopDomain.Exceptions;

public class DuplicateImageException(string fileName) : 
    Exception($"Image {fileName} is Duplicate")
{
}