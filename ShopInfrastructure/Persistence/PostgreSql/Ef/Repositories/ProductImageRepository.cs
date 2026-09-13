using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Repositories;

public class ProductImageRepository(ShopDbContext context) : RepositoryBase<ProductImage>(context),IProductImageRepository
{
}