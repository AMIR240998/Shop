namespace ShopApi.Constants;

public class DiscountUrlConstant
{
    private const string Controller = "discount";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Remove = $"{Controller}/{{id}}";
}