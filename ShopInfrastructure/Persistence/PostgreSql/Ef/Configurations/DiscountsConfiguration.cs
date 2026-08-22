using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class DiscountsConfiguration : IEntityTypeConfiguration<Discounts>
{
    public void Configure(EntityTypeBuilder<Discounts> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasMaxLength(DiscountsContants.MaxCodeLength).IsRequired();
        builder.Property(x => x.Percent).IsRequired().HasDefaultValue(DiscountsContants.HasPercentDefault);
        builder.Property(x => x.ExpireDate).IsRequired();
        builder.Property(x => x.MaxUse).IsRequired();
    }
}