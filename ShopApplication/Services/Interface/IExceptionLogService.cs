using ShopApplication.DTOs;

namespace ShopApplication.Services.Interface;

public interface IExceptionLogService
{
     Task<IReadOnlyList<ExceptionLogDto>> GetAllAsync(CancellationToken cancellationToken = default);


     Task<ExceptionLogDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
 

     Task<IReadOnlyList<ExceptionLogDto>> GetRecentlyAsync(int count, CancellationToken cancellationToken = default);
  

     Task RemoveAllAsync(CancellationToken cancellationToken = default);
   
}