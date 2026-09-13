using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.UserId).IsRequired();
        builder.Property(w => w.Balance).HasDefaultValue(WalletConstant.HasBalanceDefaultValue).IsRequired();
        builder.Property(w => w.IsActive).HasDefaultValue(WalletConstant.IsActiveDefaultValue).IsRequired();
        builder.Property(w => w.CreateAt).IsRequired();
        builder.Property(w => w.UpdateAt).IsRequired(false);
        
        builder.HasMany(w => w.WalletTransactions)
            .WithOne()
            .HasForeignKey(w => w.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}