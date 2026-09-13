using ShopApi.Contracts.Categories;
using ShopApplication.DTOs;
using ShopDomain.Entities;

namespace ShopApi.Mappers;

public static class CategoryContractMapping
{
    public static CategoryResponse MapToResponse(this CategoryDto categoryDto) => new(
        categoryDto.Id,
        categoryDto.Title);
}