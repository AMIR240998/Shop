namespace ShopDomain.Entities;

public class Products
{
    public long Id { get;private set; }
    
    public string Name { get;private set; }
    
    public string Description { get;private set; }
    
    public decimal Price { get;private set; }
    
    public int? Stock { get;private set; }
    
    public string ImageUrl { get;private set; }
    
    public int CategoryId { get;private set; }
    
    public bool IsActive { get;private set; }

    public bool IsDelete { get;private set; }
    
    public DateTimeOffset CreatedAt { get;private set; }
    
    public DateTimeOffset? UpdatedAt { get;private set; }



    private Products()
    {
    }


    public Products(long id, string name, string description, decimal price, int? stock, string imageUrl, int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
        IsActive = true;
        IsDelete = false;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(string name,string description,decimal price,int? stock,string imageUrl,int categoryId)
    {
        Name  = name;
        Description = description;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
    }
    
    public void SetUpdteAt(DateTimeOffset updateAt)
    {
        UpdatedAt = updateAt;
    }

    public void Delete()
    {
        IsDelete = true;
    }

    public static Products Rehydrate(
        long id,
        string name,
        string description,
        decimal price,
        int? stock,
        string imageUrl,
        int categoryId,
        bool isActive,
        bool isDelete,
        DateTimeOffset? createdAt,
        DateTimeOffset? updatedAt)
    {
        var product = new Products();

        product.Id = id;
        product.Name =  name;
        product.Description = description;
        product.Price =  price;
        product.Stock = stock;
        product.ImageUrl = imageUrl;
        product.CategoryId = categoryId;
        product.IsActive = isActive;
        product.IsDelete = isDelete;
        product.CreatedAt = (DateTimeOffset)createdAt!;
        product.UpdatedAt = updatedAt;
        
        return product;
    }
}