namespace ShopApi.Constants;

public abstract class AddressUrlConstant
{
    private const string Controller = "customer/address";

    public const string GetAll = $"{Controller}/{{userId}}";

    public const string GetById = $"{Controller}/{{userId}}/{{addressId}}";
    
    public const string Add = $"{Controller}";
    
    public const string Update = $"{Controller}/{{id}}";
    
    public const string Remove = $"{Controller}/{{userId}}/{{addressId}}";
}