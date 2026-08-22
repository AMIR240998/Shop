using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Dapper;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopInfrastructure.Persistence.PostgreSql.Dapper.Repositories;

public class DapperProductsRepository(IDbConnectionFactory connectionFactory) : IProductRepository
{
    private readonly List<Products> _pendingInserts = [];
    private readonly List<Products> _pendingUpdates = [];
    private readonly List<Products> _pendingRemoves = [];

    public async Task<IReadOnlyList<Products>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = $"SELECT * FROM  {nameof(Products)}";
        var connection = connectionFactory.CreateConnection();
        var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
        var rows = await connection.QueryAsync(command);
        return rows.Select(MapToProduct).ToList();
    }


    public async Task<Products?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT * " +
                           $"FROM {nameof(Products)} " +
                           "WHERE Id = @Id";

        var connection = connectionFactory.CreateConnection();
        var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
        var row = await connection.QuerySingleOrDefaultAsync<Products>(command);
        return row is null ? null : MapToProduct(row);
    }

    public Task AddAsync(Products entity, CancellationToken cancellationToken = default)
    {
        _pendingInserts.Add(entity);
        return Task.CompletedTask;
    }

    public void Update(Products entity)
    {
        _pendingUpdates.Add(entity);
    }

    public void Remove(Products entity)
    {
        _pendingRemoves.Add(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var affected = 0;
        using (var connection = connectionFactory.CreateConnection())
        {
            const string insertSql = $"INSERT INTO {nameof(Products)}" +
                                     $"(Name,Description,Price,Stock,ImageUrl,CategoryId,IsActive,IsDelete,CreatedAt)" +
                                     "VALUES(@Name,@Description,@Price,@Stock,@ImageUrl,@CategoryId,@IsActive,@IsDelete,@CreatedAt)";

            foreach (var product in _pendingInserts)
            {
                var command = new CommandDefinition(insertSql, ToInsertParams(product),
                    cancellationToken: cancellationToken);
                affected += await connection.ExecuteAsync(command);
            }

            const string updateSql = $"UPDATE {nameof(Products)}" +
                                     "SET Name = @Name , Description = @Description , Price = @Price , Stock = @Stock,ImageUrl = @ImageUrl , CategoryId = @CategoryId, " +
                                     "WHERE Id = @Id";

            foreach (var product in _pendingUpdates)
            {
                var command = new CommandDefinition(updateSql, ToUpdateParams(product),
                    cancellationToken: cancellationToken);
                affected += await connection.ExecuteAsync(command);
            }

            const string removeSql = $"DELETE FROM {nameof(Products)} WHERE Id = @Id";
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

    public async Task<IReadOnlyList<Products>> GetAllAsync(string? searchPhase, long? categoryId, long? maxPrice,
        CancellationToken cancellationToken = default)
    {
        var sql = new StringBuilder();
        sql.Append($"select * from {nameof(Products)} where 1=1");
        
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


    private static object ToInsertParams(Products entity) => new
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

    private static object ToUpdateParams(Products entity) => new
    {
        entity.Name,
        entity.Description,
        entity.Price,
        entity.Stock,
        entity.ImageUrl,
        entity.CategoryId,
    };

    private static Products MapToProduct(dynamic row)
    {
        var product = (Products)RuntimeHelpers.GetUninitializedObject(typeof(Products));
        SetPrivateProperty(product, nameof(Products.Id), (long)row.Id);
        SetPrivateProperty(product, nameof(Products.Name), (string)row.Name);
        SetPrivateProperty(product, nameof(Products.Description), (string)row.Description);
        SetPrivateProperty(product, nameof(Products.Price), (decimal)row.Price);
        SetPrivateProperty(product, nameof(Products.CategoryId), (long)row.CategoryId);
        SetPrivateProperty(product, nameof(Products.Stock), (int)row.Stock);
        SetPrivateProperty(product, nameof(Products.ImageUrl), (string)row.imageUrl);
        SetPrivateProperty(product, nameof(Products.IsActive), (bool)row.IsActive);
        SetPrivateProperty(product, nameof(Products.IsDelete), (bool)row.IsDelete);
        SetPrivateProperty(product, nameof(Products.CreatedAt), (DateTimeOffset)row.CreatedAt);
        SetPrivateProperty(product, nameof(Products.UpdatedAt), (DateTimeOffset)row.UpdatedAt);
        return product;
    }

    private static void SetPrivateProperty(object target, string propertyName, object? value)
    {
        var property = target.GetType().GetProperty(propertyName, BindingFlags.Public |
                                                                  BindingFlags.Instance | BindingFlags.NonPublic);
        property?.SetValue(target, value);
    }
}