namespace ShopApi.Constants;

public static class ProductUrlConstant
{
    private const string Controller = "product";

    public const string GetAllPaged = $"{Controller}/page";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";

    public const string GetImageProduct = $"{Controller}/image/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string UpdatePrices = $"{Controller}/{{percent}}/{{categoryId}}";
    
    public const string Remove = $"{Controller}/{{id}}";
}