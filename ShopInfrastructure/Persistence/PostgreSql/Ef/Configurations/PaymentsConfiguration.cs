using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class PaymentsConfiguration : IEntityTypeConfiguration<Payments>
{
    public void Configure(EntityTypeBuilder<Payments> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.PaymentDate).IsRequired(false);
        builder.Property(x => x.TrackingCode).IsRequired(false);
        builder.Property(x => x.Status).IsRequired();
        
        builder.HasOne<Orders>()
            .WithOne()
            .HasForeignKey<Payments>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}