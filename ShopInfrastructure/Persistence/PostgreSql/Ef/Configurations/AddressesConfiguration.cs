using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class AddressesConfiguration : IEntityTypeConfiguration<Addresses>
{
    public void Configure(EntityTypeBuilder<Addresses> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.Province).IsRequired(false);
        builder.Property(x => x.City).IsRequired(false);
        builder.Property(x => x.AddressText).IsRequired();
        builder.Property(x => x.PostalCode).IsRequired();
        
        builder.HasOne<Customers>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}