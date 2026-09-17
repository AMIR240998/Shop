using ShopApplication.DTOs.Favorite;

namespace ShopApplication.Services.Interface;

public interface IFavoriteService
{
     Task<IReadOnlyList<FavoriteDto>> GetAllAsync(CancellationToken cancellationToken = default);
  

     Task<FavoriteDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
   

     Task AddAsync(CreateFavoriteDto dto, CancellationToken cancellationToken = default);
  

     Task RemoveAsync(long id, CancellationToken cancellationToken = default);
  
}