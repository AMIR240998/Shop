using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class CommnetsConfiguration : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Text).IsRequired().HasMaxLength(CommentsConstants.MaxTextLength);
        builder.Property(x => x.Rate).IsRequired().HasDefaultValue(5);
        
        builder.HasOne<Customers>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Products>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}