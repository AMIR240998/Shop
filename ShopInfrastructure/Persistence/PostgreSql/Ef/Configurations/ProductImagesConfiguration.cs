using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class ProductImagesConfiguration : IEntityTypeConfiguration<ProductImages>
{
    public void Configure(EntityTypeBuilder<ProductImages> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(1000);
        
        builder.HasOne<Products>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}