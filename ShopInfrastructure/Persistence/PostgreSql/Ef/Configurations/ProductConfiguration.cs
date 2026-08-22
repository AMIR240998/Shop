using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(ProductsConstants.MaxNameLength);
        builder.Property(p => p.Description).HasMaxLength(ProductsConstants.MaxDescriptionLength).IsRequired(false);
        builder.Property(p => p.Price).IsRequired().HasDefaultValue(ProductsConstants.HasPriceDefault);
        builder.Property(p => p.Stock).IsRequired(false).HasDefaultValue(ProductsConstants.HasStockDefault);
        builder.Property(p => p.ImageUrl).HasMaxLength(ProductsConstants.MaxImageUrlLength).IsRequired(false);
        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.IsDelete).HasDefaultValue(false);
    }
}