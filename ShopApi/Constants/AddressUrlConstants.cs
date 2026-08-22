namespace ShopApi.Constants;

public abstract class AddressUrlConstants
{
    private const string Controller = "customer/address";

    public const string GetAll = $"{Controller}";

    public const string GetById = $"{Controller}/{{id}}";
    
    public const string Add = $"{Controller}";
}