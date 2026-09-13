namespace ShopApplication.Caching.Constants;

public static class ProductCacheKeyConstant
{
    public static string GetAllPaged(int pageNumber, int pageSize) => $"products:all:{pageNumber}:{pageSize}";
    
    public const string All = "products:all";
    
    public static string ById(long id) => $"products:{id}";
}