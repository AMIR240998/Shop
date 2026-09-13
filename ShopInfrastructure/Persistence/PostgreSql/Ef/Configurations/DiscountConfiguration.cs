using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasMaxLength(DiscountConstant.MaxCodeLength).IsRequired();
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Percent).IsRequired().HasDefaultValue(DiscountConstant.HasPercentDefault)
            .HasMaxLength(DiscountConstant.MaxPercentLength);
        builder.Property(x => x.ExpireDate).IsRequired();
        builder.Property(x => x.MaxUse).IsRequired();
    }
}