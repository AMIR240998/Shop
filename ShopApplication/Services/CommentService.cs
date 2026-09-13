using ShopApplication.DTOs;
using ShopApplication.DTOs.Comment;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class CommentService(ICommentRepository repository)
{
    public async Task<IReadOnlyList<CommentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var comments = await repository.GetAllAsync(cancellationToken);
        return comments.Select(ToDto).ToList();
    }
    public async Task<IReadOnlyList<CommentDto>> GetAllByProductIdAsync(long productId,CancellationToken cancellationToken = default)
    {
        var comments = await repository.GetAllByProductIdAsync(productId,cancellationToken);
        return comments.Select(ToDto).ToList();
    }
    public async Task<IReadOnlyList<CommentDto>> GetAllByCustomerIdAsync(long customerId,CancellationToken cancellationToken = default)
    {
        var comments = await repository.GetAllByCustomerIdAsync(customerId,cancellationToken);
        return comments.Select(ToDto).ToList();
    }

    public async Task<CommentDto> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var comment = await repository.GetByIdAsync(id, cancellationToken)
                      ?? throw new EntityNotFoundException(nameof(Comment), id);
        return ToDto(comment);
    }

    public async Task AddAsync(CreateCommentDto dto, CancellationToken cancellationToken = default)
    {
        var comments = new Comment(dto.UserId,dto.ProductId ,dto.Text, dto.Rate);
        await repository.AddAsync(comments, cancellationToken);
    }

    public async Task UpdateAsync(string id,UpdateCommentDto dto, CancellationToken cancellationToken = default)
    {
        var comments = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Comment), id);
        
        comments.Update(dto.Text, dto.Rate);
        await repository.Update(comments);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var comments = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Comment), id);
        await repository.Remove(id, cancellationToken);
    }

    private static CommentDto ToDto(Comment comment) => new(
        comment.Id,
        comment.UserId,
        comment.ProductId,
        comment.Text,
        comment.Rate,
        comment.CreatedAt);
}