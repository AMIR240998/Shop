using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.CreateAt).IsRequired();
        builder.Property(b => b.UpdateAt).IsRequired(false);
        
        builder.HasMany(b => b.Items)
            .WithOne()
            .HasForeignKey(b => b.BasketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}