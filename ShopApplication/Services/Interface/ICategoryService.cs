using ShopApplication.DTOs;

namespace ShopApplication.Services.Interface;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    void RemoveAllAsync(CancellationToken cancellationToken = default);
}