using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class Category
{
    public int Id { get;private set; }

    public string Title { get;private set; }

    private Category()
    {
    }
    
    public Category(CategoryType title)
    {
        Title = title.ToString();
    }
}