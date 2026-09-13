using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasDefaultValue(PaymentConstant.HasAmountDefault);
        builder.Property(x => x.PaymentDate).IsRequired(false);
        builder.Property(x => x.TrackingCode).IsRequired(false);
        builder.Property(x => x.Status).IsRequired().HasMaxLength(PaymentConstant.HasStatusMaxLength);
        
        builder.HasOne<Order>()
            .WithOne()
            .HasForeignKey<Payment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}