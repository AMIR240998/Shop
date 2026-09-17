using ShopApplication.DTOs.Order;
using ShopApplication.Repositories;
using ShopApplication.Services.Interface;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services.Implementation;

public class OrderService(
    IOrderRepository orderRepository) : IOrderService 
{
    public async Task<IReadOnlyList<OrderDto>> GetAllOrders(CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);
        return orders.Select(ToDto).ToList();
    }

    public async Task<OrderDto?> GetOrderById(long id, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetOrderByIdAsync(id, cancellationToken)
                    ?? throw new EntityNotFoundException(nameof(Order), id);
        return ToDto(order);
    }

    public async Task RemoveOrders(long orderId, CancellationToken cancellationToken = default)
    {
        var order = await  orderRepository.GetOrderByIdAsync(orderId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Order), orderId);
        orderRepository.Remove(order, cancellationToken);
        await orderRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OrderItemDto>> GetAllItemsByIdAsync(long orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId, cancellationToken)
                 ?? throw new EntityNotFoundException(nameof(Order), orderId);
        
        var items = order.Items.ToList();
        
        return items.Select(ToItemDto).ToList();
    }

    public async Task<OrderItemDto?> GetItemByIdAsync(long orderId, long itemId,
        CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Order), orderId);
        
        var item =  order.Items.FirstOrDefault(i => i.Id == itemId)
            ??  throw new EntityNotFoundException(nameof(OrderItem), itemId);
        
        return ToItemDto(item);
    }

    
    private static OrderDto ToDto(Order order) => new(
        order.Id,
        order.UserId,
        order.OrderDate,
        order.Status,
        order.TotalPrice);
    
    private static OrderItemDto ToItemDto(OrderItem? items) => new(
        items.Id,
        items.OrderId,
        items.ProductId,
        items.Count,
        items.Price);
        
}