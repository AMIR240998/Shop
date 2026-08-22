using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ShopInfrastructure.Persistence.PostgreSql.Dapper;

public class SqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
        private readonly string _connectionString = 
            configuration.GetConnectionString("PostgreSql")
                ?? throw new InvalidOperationException("Connection string not found");

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

}