using System.Text;
using Amazon.Runtime;
using Amazon.S3;
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
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Seed;
using ShopInfrastructure.Persistence.SqlServer.Ef.Repositories;
using ShopInfrastructure.services;
using ShopInfrastructure.services.FileStorage;
using ShopInfrastructure.services.Payment;

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

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentGateway, FakePaymentGateway>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        
        services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<SeedData>();
        
        services.AddMongo(configuration);
        services.AddRedis(configuration);
        
        services.AddArvanCloud(configuration);
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
        services.Configure<RedisSetting>(
            configuration.GetSection(RedisSetting.SectionName));

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

    private static void AddArvanCloud(this IServiceCollection services, IConfiguration configuration)
    {
        var accessKey = configuration["ArvanCloud:AccessKey"];

        var secretKey = configuration["ArvanCloud:SecretKey"];

        var serviceUrl = configuration["ArvanCloud:ServiceUrl"];

        var credentials = new BasicAWSCredentials(
            accessKey,
            secretKey);
    
        var s3Config = new AmazonS3Config
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true
        };

        services.AddSingleton<IAmazonS3>(
            new AmazonS3Client(credentials, s3Config));

        services.AddScoped<IFileStorage, ArvanCloudFileStorage>();
    }
}