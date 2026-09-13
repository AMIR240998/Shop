using Microsoft.EntityFrameworkCore;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

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

    public DbSet<Product> Products { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<Payment> Payments { get; set; }
    
    public DbSet<Address> Addresses { get; set; }
    
    // public DbSet<Comments> Comments { get; set; }
    
    public DbSet<Discount> Discounts { get; set; }
    
    public DbSet<Favorite> Favorites { get; set; }
    
    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<Basket> Baskets { get; set; }
    
    public DbSet<BasketItem> BasketItems { get; set; }
    
    public DbSet<Wallet> Wallets { get; set; }
    
    public DbSet<WalletTransaction> WalletTransactions { get; set; }
}