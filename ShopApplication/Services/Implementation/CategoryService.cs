using ShopApplication.DTOs;
using ShopApplication.Repositories;
using ShopApplication.Services.Interface;
using ShopDomain.Entities;

namespace ShopApplication.Services.Implementation;

public class CategoryService(ICategoryRepository repository) : ICategoryService
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