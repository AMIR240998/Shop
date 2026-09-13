namespace ShopApi.Constants;

public class BasketUrlConstant
{
    private const string Controller = "basket";

    public const string GetItemsById = $"{Controller}/{{basketId}}";

    public const string GetItemById = $"{Controller}/{{basketId}}/{{itemId}}";
    
    public const string AddItem = $"{Controller}";
    
    public const string RemoveItem = $"{Controller}/{{basketId}}/{{itemId}}";
    
}