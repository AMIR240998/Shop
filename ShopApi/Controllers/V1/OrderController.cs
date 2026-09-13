using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Order;
using ShopApi.Contracts.Orders;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;
[ApiVersion(1.0)]
public class OrderController(OrderService service) : BaseController
{
    [HttpGet(OrderUrlConstant.GetAllOrders)]
    public async Task<ApiResult<IReadOnlyList<OrderResponse>>> GetAllOrders(CancellationToken cancellationToken = default)
    {
        var orders = await service.GetAllOrders(cancellationToken);
        return orders.Select(o => o.ToResponse()).ToList();
    }

    [HttpGet(OrderUrlConstant.GetOrderById)]
    public async Task<ApiResult<OrderResponse>> GetOrderById(long orderId,
        CancellationToken cancellationToken = default)
    {
        var order = await service.GetOrderById(orderId, cancellationToken);
        return order.ToResponse();
    }

    // [HttpPost(OrderUrlConstant.AddOrder)]
    // public async Task<ApiResult<OrderResponse>> AddOrder(long customerId,List<CreateOrderItemRequest> request,CancellationToken cancellationToken = default)
    // {
    //     var order = await service.AddOrdersAsync(customerId, request.ToDto(), cancellationToken);
    //     return order.ToResponse();
    // }

    [HttpDelete(OrderUrlConstant.RemoveOrder)]
    public async Task<ApiResult> RemoveOrder(long orderId, CancellationToken cancellationToken = default)
    {
        await service.RemoveOrders(orderId, cancellationToken);
        return ApiResult.Secceded();
    }

    // [HttpGet(OrderUrlConstant.GetAllOrdersItem)]
    // public async Task<ApiResult<IReadOnlyList<OrderItemResponse>>> GetAllOrdersItems(
    //     CancellationToken cancellationToken = default)
    // {
    //     var orderItems = await service.GetAllOrderItems(cancellationToken);
    //     return orderItems.Select(o => o.ToItemResponse()).ToList();
    // }

    [HttpGet(OrderUrlConstant.GetAllItemsById)]
    public async Task<ApiResult<IReadOnlyList<OrderItemResponse>>> GetAllItemsById(long orderId,
        CancellationToken cancellationToken = default)
    {
        var orderItems = await service.GetAllItemsByIdAsync(orderId,cancellationToken);
        return orderItems.Select(o => o.ToItemResponse()).ToList();
    }

    [HttpGet(OrderUrlConstant.GetItemById)]
    public async Task<ApiResult<OrderItemResponse>> GetItemById(long orderId, long itemId,
        CancellationToken cancellationToken = default)
    {
        var orderItem = await service.GetItemByIdAsync(orderId, itemId, cancellationToken);
        return orderItem.ToItemResponse();
    }
}