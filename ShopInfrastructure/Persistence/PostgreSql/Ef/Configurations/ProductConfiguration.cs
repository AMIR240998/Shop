using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(ProductConstant.MaxNameLength);
        builder.Property(p => p.Description).HasMaxLength(ProductConstant.MaxDescriptionLength).IsRequired(false);
        builder.Property(p => p.Price).IsRequired().HasDefaultValue(ProductConstant.HasPriceDefault);
        builder.Property(p => p.Stock).IsRequired(false).HasDefaultValue(ProductConstant.HasStockDefault);
        builder.Property(p => p.ImageUrl).HasMaxLength(ProductConstant.MaxImageUrlLength).IsRequired(false);
        builder.Property(p => p.ContentTypeImage).HasMaxLength(ProductConstant.MaxContentTypeLength).IsRequired(false);
        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.IsDelete).HasDefaultValue(false);
        
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(p => p.ProductImages)
            .WithOne()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}