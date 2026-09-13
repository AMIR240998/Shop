namespace ShopApi.Constants;

public abstract class UserUrlConstant
{
    private const string Controller = "user";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{id}}";
    
    public const string Exist = $"{Controller}/exist/{{userName}}";
    
    public const string Login = $"{Controller}/login";
}