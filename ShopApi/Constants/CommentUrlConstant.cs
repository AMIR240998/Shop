namespace ShopApi.Constants;

public class CommentUrlConstant
{
    private const string Controller = "comment";

    public const string GetAll = $"{Controller}";
    
    public const string GetAllByCustomerId = $"{Controller}/customerId/{{id}}";
    
    public const string GetAllByProductId = $"{Controller}/productId/{{id}}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{id}}";
}