using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopApplication.Caching;
using ShopApplication.Repositories;
using ShopInfrastructure.Caching.Redis;
using ShopInfrastructure.Persistence.MongoDb;
using ShopInfrastructure.Persistence.MongoDb.Repositories;
using ShopInfrastructure.Persistence.PostgreSql.Dapper;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Seed;
using ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;
using ShopInfrastructure.Persistence.SqlServer.Ef.Repositories;
using ShopInfrastructure.services;

namespace ShopInfrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShopDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
        
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwt = configuration.GetSection("Jwt").Get<JwtOptions>();
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt!.Issuer,
                    ValidAudience = jwt.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.Key))
                };
            });

        services.AddScoped<IProductRepository, ProductsRepository>();
        services.AddScoped<ICustomerRepository, CustomersRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICategoriesRepository, CategoryRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductImageRepository, ProductsImageRepository>();
        
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        
        services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<SeedData>();
        
        services.AddMongo(configuration);
        services.AddRedis(configuration);
    }
    private static void AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSetting>(configuration.GetSection(MongoSetting.SectionName));

        MongoMappingConfig.Register();

        services.AddSingleton<MongoContext>();
        services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();
    }

    private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        //IDistributedCache
        services.Configure<RedisSettings>(
            configuration.GetSection(RedisSettings.SectionName));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
            options.InstanceName = configuration["Redis:InstanceName"];
        });
        services.AddSingleton<ICacheService, DistributedCacheService>();
        
        //
        // services.Configure<RedisSettings>(configuration.GetSection(RedisSettings.SectionName));
        //
        // services.AddSingleton<RedisConnection>();
        // services.AddSingleton<ICacheService, DistributedCacheService>();
    }
}