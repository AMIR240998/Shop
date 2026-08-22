using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;
using ShopDomain.Enums;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class CustomersConfiguration : IEntityTypeConfiguration<Customers>
{
    public void Configure(EntityTypeBuilder<Customers> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(CustomersConstants.MaxFirstNameLength);
        builder.Property(x => x.LastName).HasMaxLength(CustomersConstants.MaxLastNameLength);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(CustomersConstants.MaxAddressLength);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(CustomersConstants.MaxEmailLength);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(CustomersConstants.MaxUserNameLength);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(CustomersConstants.MaxPasswordHashLength);
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.CreateAt).IsRequired();
    }
}