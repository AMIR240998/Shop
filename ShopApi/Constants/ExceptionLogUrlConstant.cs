namespace ShopApi.Constants;

public class ExceptionLogUrlConstant
{
    private const string Controller = "exceptionLog";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string GetRecently = $"{Controller}/recently/{{count}}";
    
    public const string Remove = $"{Controller}/RemoveAll";
}