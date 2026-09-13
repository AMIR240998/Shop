using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Categories;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;
[ApiVersion(1.0)]
public class CategoryController(CategoryService categoryService) : BaseController
{
    [HttpGet(CategoryUrlConstant.GetAll)]
    public async Task<ApiResult<List<CategoryResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var categories = await categoryService.GetAllAsync(cancellationToken);
        return categories.Select(c => c.MapToResponse()).ToList();
    }

    [HttpDelete(CategoryUrlConstant.RemoveAll)]
    public async Task<ApiResult> RemoveAll(CancellationToken cancellationToken = default)
    {
        categoryService.RemoveAllAsync(cancellationToken);
        return ApiResult.Secceded();
    }
}