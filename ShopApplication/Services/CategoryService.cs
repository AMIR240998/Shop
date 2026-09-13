using ShopApplication.DTOs;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopApplication.Services;

public class CategoryService(ICategoryRepository repository)
{
    public async Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await repository.GetAllAsync(cancellationToken);
        return categories.Select(MapToDto).ToList();
    }

    public void RemoveAllAsync(CancellationToken cancellationToken = default)
    {
        repository.RemoveAllAsync(cancellationToken);
        repository.SaveChangesAsync(cancellationToken);
    }
    
    private static CategoryDto MapToDto(Category category) => new (
        category.Id,
        category.Title);
}