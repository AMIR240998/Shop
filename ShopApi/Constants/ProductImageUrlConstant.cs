namespace ShopApi.Constants;

public class ProductImageUrlConstant
{
    private const string Controller = "productImage";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{productId}}/{{imageId}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{productId}}/{{imageId}}";
}