using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
{
    public void Configure(EntityTypeBuilder<OrderItems> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Count).IsRequired().HasDefaultValue(OrderItemsConstants.HasCountDefault);
        builder.Property(x => x.Price).IsRequired().HasDefaultValue(OrderItemsConstants.HasPriceDefault);
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        
        builder.HasOne<Orders>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Products>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}