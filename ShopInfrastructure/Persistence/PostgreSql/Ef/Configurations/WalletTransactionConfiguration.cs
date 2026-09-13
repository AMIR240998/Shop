using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.WalletId).IsRequired();
        builder.Property(w => w.Amount).IsRequired();
        builder.Property(w => w.WalletId).IsRequired();
        builder.Property(w => w.Type).IsRequired()
            .HasMaxLength(WalletTransactionConstant.HasTypeMaxLength);
        builder.Property(w => w.Status).IsRequired()
            .HasMaxLength(WalletTransactionConstant.HasStatusMaxLength);

        builder.Property(w => w.Description).IsRequired(false)
            .HasMaxLength(WalletTransactionConstant.HasDescriptionMaxLength);
        
        builder.Property(w => w.RefrenceId).IsRequired(false)
            .HasMaxLength(WalletTransactionConstant.HasRefrenceIdMaxLength);
        
        builder.Property(w => w.CreateAt).IsRequired();
        
    }
}