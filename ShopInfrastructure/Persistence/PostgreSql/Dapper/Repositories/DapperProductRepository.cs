using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Dapper;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Dapper.Repositories;

public class DapperProductRepository(IDbConnectionFactory connectionFactory) : IProductRepository
{
    private readonly List<Product> _pendingInserts = [];
    private readonly List<Product> _pendingUpdates = [];
    private readonly List<Product> _pendingRemoves = [];

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = $"SELECT * FROM  {nameof(Product)}";
        var connection = connectionFactory.CreateConnection();
        var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
        var rows = await connection.QueryAsync(command);
        return rows.Select(MapToProduct).ToList();
    }


    public async Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT * " +
                           $"FROM {nameof(Product)} " +
                           "WHERE Id = @Id";

        var connection = connectionFactory.CreateConnection();
        var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
        var row = await connection.QuerySingleOrDefaultAsync<Product>(command);
        return row is null ? null : MapToProduct(row);
    }

    public Task AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        _pendingInserts.Add(entity);
        return Task.CompletedTask;
    }

    public void Update(Product entity)
    {
        _pendingUpdates.Add(entity);
    }

    public void Remove(Product entity)
    {
        _pendingRemoves.Add(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var affected = 0;
        using (var connection = connectionFactory.CreateConnection())
        {
            const string insertSql = $"INSERT INTO {nameof(Product)}" +
                                     $"(Name,Description,Price,Stock,ImageUrl,CategoryId,IsActive,IsDelete,CreatedAt)" +
                                     "VALUES(@Name,@Description,@Price,@Stock,@ImageUrl,@CategoryId,@IsActive,@IsDelete,@CreatedAt)";

            foreach (var product in _pendingInserts)
            {
                var command = new CommandDefinition(insertSql, ToInsertParams(product),
                    cancellationToken: cancellationToken);
                affected += await connection.ExecuteAsync(command);
            }

            const string updateSql = $"UPDATE {nameof(Product)}" +
                                     "SET Name = @Name , Description = @Description , Price = @Price , Stock = @Stock,ImageUrl = @ImageUrl , CategoryId = @CategoryId, " +
                                     "WHERE Id = @Id";

            foreach (var product in _pendingUpdates)
            {
                var command = new CommandDefinition(updateSql, ToUpdateParams(product),
                    cancellationToken: cancellationToken);
                affected += await connection.ExecuteAsync(command);
            }

            const string removeSql = $"DELETE FROM {nameof(Product)} WHERE Id = @Id";
            foreach (var product in _pendingRemoves)
            {
                var command =
                    new CommandDefinition(removeSql, new { product.Id }, cancellationToken: cancellationToken);
                affected += await connection.ExecuteAsync(command);
            }

            _pendingInserts.Clear();
            _pendingUpdates.Clear();
            _pendingRemoves.Clear();
        }
    }

    public Task<(IReadOnlyList<Product> productsList, int count)> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(string? searchPhase, long? categoryId, long? maxPrice,
        CancellationToken cancellationToken = default)
    {
        var sql = new StringBuilder();
        sql.Append($"select * from {nameof(Product)} where 1=1");
        
        if (!string.IsNullOrEmpty(searchPhase))
             sql.Append(" And Name LIKE @SearchPhase");
        
        if(categoryId is not null)
            sql.Append(" And CategoryId = @CategoryId");
        
        if(maxPrice is not null)
            sql.Append(" And MaxPrice <= @MaxPrice");
        
        var connection = connectionFactory.CreateConnection();
        var command = new CommandDefinition(sql.ToString() ,new {@SearchPhase = searchPhase, @CategoryId = categoryId , 
            @MaxPrice = maxPrice }, cancellationToken: cancellationToken);
        var rows = await connection.QueryAsync(command);
        return rows.Select(MapToProduct).ToList();
    }

    public Task<IReadOnlyList<Product>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Product>> UpdatePriceAsync(decimal percent, int categoryId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    


    private static object ToInsertParams(Product entity) => new
    {
        entity.Name,
        entity.Description,
        entity.Price,
        entity.Stock,
        entity.ImageUrl,
        entity.CategoryId,
        entity.IsActive,
        entity.IsDelete,
        entity.CreatedAt
    };

    private static object ToUpdateParams(Product entity) => new
    {
        entity.Name,
        entity.Description,
        entity.Price,
        entity.Stock,
        entity.ImageUrl,
        entity.CategoryId,
    };

    private static Product MapToProduct(dynamic row)
    {
        var product = (Product)RuntimeHelpers.GetUninitializedObject(typeof(Product));
        SetPrivateProperty(product, nameof(Product.Id), (long)row.Id);
        SetPrivateProperty(product, nameof(Product.Name), (string)row.Name);
        SetPrivateProperty(product, nameof(Product.Description), (string)row.Description);
        SetPrivateProperty(product, nameof(Product.Price), (decimal)row.Price);
        SetPrivateProperty(product, nameof(Product.CategoryId), (long)row.CategoryId);
        SetPrivateProperty(product, nameof(Product.Stock), (int)row.Stock);
        SetPrivateProperty(product, nameof(Product.ImageUrl), (string)row.imageUrl);
        SetPrivateProperty(product, nameof(Product.IsActive), (bool)row.IsActive);
        SetPrivateProperty(product, nameof(Product.IsDelete), (bool)row.IsDelete);
        SetPrivateProperty(product, nameof(Product.CreatedAt), (DateTimeOffset)row.CreatedAt);
        SetPrivateProperty(product, nameof(Product.UpdatedAt), (DateTimeOffset)row.UpdatedAt);
        return product;
    }

    private static void SetPrivateProperty(object target, string propertyName, object? value)
    {
        var property = target.GetType().GetProperty(propertyName, BindingFlags.Public |
                                                                  BindingFlags.Instance | BindingFlags.NonPublic);
        property?.SetValue(target, value);
    }
}