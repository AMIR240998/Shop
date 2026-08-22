using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderDate).IsRequired(false);
        builder.Property(x => x.Status).IsRequired();
        
        builder.HasOne<Customers>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}