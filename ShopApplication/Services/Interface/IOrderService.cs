using ShopApplication.DTOs.Order;

namespace ShopApplication.Services.Interface;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> GetAllOrders(CancellationToken cancellationToken = default);


    Task<OrderDto?> GetOrderById(long id, CancellationToken cancellationToken = default);


    Task RemoveOrders(long orderId, CancellationToken cancellationToken = default);


    Task<IReadOnlyList<OrderItemDto>> GetAllItemsByIdAsync(long orderId, CancellationToken cancellationToken = default);


    Task<OrderItemDto?> GetItemByIdAsync(long orderId, long itemId, CancellationToken cancellationToken = default);

}