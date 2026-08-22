using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class FavoritesConfiguration : IEntityTypeConfiguration<Favorites>
{
    public void Configure(EntityTypeBuilder<Favorites> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        
        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}