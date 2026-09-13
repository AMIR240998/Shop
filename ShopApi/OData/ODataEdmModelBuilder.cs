using Microsoft.OData.ModelBuilder;
using ShopDomain.Entities;

namespace ShopApi.OData;

public static class ODataEdmModelBuilder
{
    public static Microsoft.OData.Edm.IEdmModel CreateEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        var products = builder.EntitySet<User>("ODataCustomer");
        var product = products.EntityType;
        product.HasKey(p => p.Id);
        product.Property(p => p.FirstName);
        product.Property(p => p.LastName);
        product.Property(p => p.Email);
        product.Property(p => p.UserName);
        product.Property(p => p.PasswordHash);
        product.Property(p => p.Address);
        product.Property(p => p.CreatedAt);
        
        return builder.GetEdmModel();
    }
}