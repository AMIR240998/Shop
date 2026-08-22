using Microsoft.EntityFrameworkCore;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

public class ShopDbContext : DbContext
{

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
    }

    public DbSet<Products> Products { get; set; }
    
    public DbSet<Categories> Categories { get; set; }
    
    public DbSet<Customers> Customers { get; set; }
    
    public DbSet<Orders> Orders { get; set; }
    
    public DbSet<OrderItems> OrderItems { get; set; }
    
    public DbSet<Payments> Payments { get; set; }
    
    public DbSet<Addresses> Addresses { get; set; }
    
    public DbSet<Comments> Comments { get; set; }
    
    public DbSet<Discounts> Discounts { get; set; }
    
    public DbSet<Favorites> Favorites { get; set; }
    
    public DbSet<ProductImages> ProductImages { get; set; }
}