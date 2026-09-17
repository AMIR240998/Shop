using ShopApplication.DTOs;
using ShopApplication.DTOs.Comment;

namespace ShopApplication.Services.Interface;

public interface ICommentService
{
    Task<IReadOnlyList<CommentDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CommentDto>>
        GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CommentDto>> GetAllByCustomerIdAsync(long customerId,
        CancellationToken cancellationToken = default);


    Task<CommentDto> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task AddAsync(CreateCommentDto dto, CancellationToken cancellationToken = default);


    Task UpdateAsync(string id, UpdateCommentDto dto, CancellationToken cancellationToken = default);


    Task RemoveAsync(string id, CancellationToken cancellationToken = default);
}