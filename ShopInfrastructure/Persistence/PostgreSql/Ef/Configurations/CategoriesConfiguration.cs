using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Constants;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).HasMaxLength(CategoriesConstants.MaxTitleLength).IsRequired();
    }
}