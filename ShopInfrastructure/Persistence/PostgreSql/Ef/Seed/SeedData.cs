using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Enums;
using ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Seed;

public class SeedData(ShopDbContext context,IPasswordHasher passwordHasher) : ISeedData
{
    public async Task SeedAsync()
    {
        var result = await context.Customers.AnyAsync(c => c.Role == UserRole.Admin);
        if (!result)
        {
            var passWordHash = passwordHasher.HashPassword("amir.ali.1388");
            
            var customer = new Customers("AmirAli", "Azimi", "amir.ali.azimi@gmail.com", passWordHash,
                "Kashan", "amir.ali.1388",
                UserRole.Admin);

            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }
    }
}