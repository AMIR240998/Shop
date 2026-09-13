using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Comment;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;

public class CommentController(CommentService service) : BaseController
{
    [HttpGet(CommentUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<CommentResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var comments = await service.GetAllAsync(cancellationToken);
        return comments.Select(c => c.ToResponse()).ToList();
    }
    [HttpGet(CommentUrlConstant.GetAllByCustomerId)]
    public async Task<ApiResult<IReadOnlyList<CommentResponse>>> GetAllByCustomerId(long customerId,CancellationToken cancellationToken = default)
    {
        var comments = await service.GetAllByCustomerIdAsync(customerId,cancellationToken);
        return comments.Select(c => c.ToResponse()).ToList();
    }
    [HttpGet(CommentUrlConstant.GetAllByProductId)]
    public async Task<ApiResult<IReadOnlyList<CommentResponse>>> GetAllByProductId(long productId,CancellationToken cancellationToken = default)
    {
        var comments = await service.GetAllByProductIdAsync(productId,cancellationToken);
        return comments.Select(c => c.ToResponse()).ToList();
    }

    [HttpGet(CommentUrlConstant.GetById)]
    public async Task<ApiResult<CommentResponse>> GetById(string id, CancellationToken cancellationToken = default)
    {
        var comment = await service.GetByIdAsync(id, cancellationToken);
        return comment.ToResponse();
    }

    [HttpPost(CommentUrlConstant.Add)]
    public async Task<ApiResult> Add(CreateCommentRequest request,CancellationToken cancellationToken =  default)
    {
        await service.AddAsync(request.ToCreateCommentDto(), cancellationToken);
        return ApiResult.Secceded();
    }

    [HttpPut(CommentUrlConstant.Update)]
    public async Task<ApiResult> Update(string id,UpdateCommentRequest request, CancellationToken cancellationToken = default)
    {
        await service.UpdateAsync(id,request.ToUpdateCommentDto(),cancellationToken);
        return ApiResult.Secceded();
    }

    [HttpDelete(CommentUrlConstant.Remove)]
    public async Task<ApiResult> Remove(string id, CancellationToken cancellationToken = default)
    {
        await service.RemoveAsync(id, cancellationToken);
        return ApiResult.Secceded();
    }
}