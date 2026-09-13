using Microsoft.EntityFrameworkCore;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Enums;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Seed;

public class SeedData(ShopDbContext context, IPasswordHasher passwordHasher) : ISeedData
{
    public async Task SeedAsync()
    {
        var result = await context.Users.AnyAsync(c => c.Role == UserRole.Admin);
        if (!result)
        {
            var passWordHash = passwordHasher.HashPassword("amir.ali.1388");

            var customer = new User("AmirAli", "Azimi", "amir.ali.azimi@gmail.com", passWordHash,
                "Kashan", "amir.ali.1388",
                UserRole.Admin);
            await context.Users.AddAsync(customer);
        }

        var categories = await context.Categories.ToListAsync();
        if (categories.Count == 0)
        {
            foreach (var category in Enum.GetValues<CategoryType>())
            {
                var entity = new Category(category);
                await context.Categories.AddAsync(entity);
            }
        }
        await context.SaveChangesAsync();
    }
}