using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShopApplication.Caching;
using ShopApplication.Services;

namespace ShopApplication;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddScoped<ProductService>();
        services.AddScoped<ProductCache>();
        services.AddScoped<CategoryService>();
        services.AddScoped<OrderService>();
        services.AddScoped<FavoriteService>();
        services.AddScoped<ProductImageService>();
        services.AddScoped<CustomerService>();
        // services.AddScoped<AddressService>();
    }
}