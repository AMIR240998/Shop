namespace ShopApi.Constants;

public class CategoryUrlConstant
{
    private const string Controller = "category";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{id}}";
    
    public const string RemoveAll = $"{Controller}";
    
}