using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShopApplication.Caching;
using ShopApplication.Services;
using ShopApplication.Services.Implementation;
using ShopApplication.Services.Interface;
using ShopApplication.Services.Interfaces;

namespace ShopApplication;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddScoped<IProductService,ProductService>();
        services.AddScoped<ProductCache>();
        services.AddScoped<ICategoryService,CategoryService>();
        services.AddScoped<IOrderService,OrderService>();
        services.AddScoped<IFavoriteService,FavoriteService>();
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<IExceptionLogService,ExceptionLogService>();
        services.AddScoped<ICommentService,CommentService>();
        services.AddScoped<PaymentService>();
        services.AddScoped<IBasketService,BasketService>();
        services.AddScoped<IDiscountService,DiscountService>();
    }
}