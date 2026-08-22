using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;
using ShopInfrastructure.Persistence.SqlServer.Ef.Configurations;

namespace ShopInfrastructure.Persistence.SqlServer.Ef.Repositories;

public class CategoryRepository(ShopDbContext context) : RepositoryBase<Categories>(context),ICategoriesRepository
{
}