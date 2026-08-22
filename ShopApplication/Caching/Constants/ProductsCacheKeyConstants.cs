namespace ShopApplication.Caching.Constants;

public static class ProductsCacheKeyConstants
{
    public const string All = "products:all";
    
    public static string ById(long id) => $"products:{id}";
}