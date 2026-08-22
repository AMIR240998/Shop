namespace ShopApi.Constants;

public abstract class CustomersUrlConstants
{
    private const string Controller = "customer";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{id}}";
    
    public const string Exist = $"{Controller}/exist/{{userName}}";
    
    public const string Login = $"{Controller}/login";
}