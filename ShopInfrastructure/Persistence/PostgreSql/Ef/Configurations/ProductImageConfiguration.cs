using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(ProductImageConstant.HasImageUrlMaxLength);
        builder.Property(x => x.ContentTypeImage).HasMaxLength(ProductImageConstant.HasContentTypeMaxLength)
            .IsRequired(false);
    }
}