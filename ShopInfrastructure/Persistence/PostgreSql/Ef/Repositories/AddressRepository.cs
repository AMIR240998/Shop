using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopInfrastructure.Persistence.PostgreSql.Ef.Configurations;

namespace ShopInfrastructure.Persistence.PostgreSql.Ef.Repositories;

public class AddressRepository(ShopDbContext context) : RepositoryBase<Address>(context),IAddressRepository
{
}