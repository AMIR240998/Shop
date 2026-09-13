namespace ShopApi.Constants;

public class FavoriteUrlConstant
{
    private const string Controller = "favorite";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Remove = $"{Controller}/{{id}}";
}