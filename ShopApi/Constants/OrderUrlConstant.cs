namespace ShopApi.Constants;

public class OrderUrlConstant
{
    private const string Controller = "order";

    public const string GetAllOrders = $"{Controller}";

    public const string GetOrderById = $"{Controller}/{{id}}";
    
    public const string AddOrder = $"{Controller}";
    
    public const string RemoveOrder = $"{Controller}/{{id}}";
    
    public const string GetAllOrdersItem = $"{Controller}/items";
    
    public const string GetAllItemsById = $"{Controller}/items/{{orderId}}";
    
    public const string GetItemById = $"{Controller}/{{orderId}}/{{itemId}}";
    
}