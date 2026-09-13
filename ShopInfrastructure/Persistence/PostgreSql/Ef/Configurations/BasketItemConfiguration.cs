using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.BasketId).IsRequired();
        builder.Property(b => b.ProductId).IsRequired();
        builder.Property(b => b.Quantity).IsRequired()
            .HasDefaultValue(BasketItemConstant.HasQuantityDefault)
            .HasMaxLength(BasketItemConstant.HasQuantityMax);
        
        builder.HasIndex(p => new {p.ProductId,p.BasketId}).IsUnique();
        
        builder.HasOne(b => b.Product)
            .WithMany()
            .HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}