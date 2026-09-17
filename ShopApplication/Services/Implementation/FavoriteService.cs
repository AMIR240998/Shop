using ShopApplication.DTOs.Favorite;
using ShopApplication.Repositories;
using ShopApplication.Services.Interface;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services.Implementation;

public class FavoriteService(IFavoriteRepository repository) : IFavoriteService
{
    public async Task<IReadOnlyList<FavoriteDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var favorites = await repository.GetAllAsync(cancellationToken);
        return favorites.Select(ToDto).ToList();
    }

    public async Task<FavoriteDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var favorite = await repository.GetByIdAsync(id, cancellationToken)
                       ?? throw new EntityNotFoundException(nameof(Favorite), id);
        return ToDto(favorite);
    }

    public async Task AddAsync(CreateFavoriteDto dto, CancellationToken cancellationToken = default)
    {
        var favorites = new Favorite(dto.UserId,dto.ProductId);
        await repository.AddAsync(favorites, cancellationToken);
        await repository.SaveChangeAsync(cancellationToken);
    }

    public async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var favorite = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Favorite), id);
         repository.RemoveAsync(favorite);
         await repository.SaveChangeAsync(cancellationToken);
    }

    private static FavoriteDto ToDto(Favorite favorite) => new(
        favorite.Id,
        favorite.UserId,
        favorite.ProductId);

}