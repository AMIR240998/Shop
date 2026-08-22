using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class Categories
{
    public int Id { get;private set; }

    public CategoryType Title { get;private set; }

    private Categories()
    {
    }
    
    public Categories(int id, CategoryType title)
    {
        Id  = id;
        Title = title;
    }
}