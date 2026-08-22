namespace ShopApi.Constants;

public static class ProductsUrlConstants
{
    private const string Controller = "product";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{id}}";
}