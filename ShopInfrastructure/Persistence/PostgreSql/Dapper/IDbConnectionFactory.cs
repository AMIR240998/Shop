using System.Data;

namespace ShopInfrastructure.Persistence.PostgreSql.Dapper;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}