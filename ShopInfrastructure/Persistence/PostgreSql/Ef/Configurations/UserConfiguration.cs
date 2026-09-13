using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(UserConstant.MaxFirstNameLength);
        builder.Property(x => x.LastName).HasMaxLength(UserConstant.MaxLastNameLength);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(UserConstant.MaxAddressLength);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(UserConstant.MaxEmailLength);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(UserConstant.MaxUserNameLength);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(UserConstant.MaxPasswordHashLength);
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        
        builder.HasOne(x => x.Basket)
            .WithOne()
            .HasForeignKey<Basket>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Wallet)
            .WithOne()
            .HasForeignKey<Wallet>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Addresses)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}