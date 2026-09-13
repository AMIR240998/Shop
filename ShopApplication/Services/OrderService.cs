using ShopApplication.DTOs.Order;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IBasketRepository basketRepository,
    IProductRepository productRepository)
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

    // public async Task<OrderDto> AddOrdersAsync(
    //     long customerId,
    //     long basketId,
    //     List<CreateOrderItemDto> itemDtos,
    //     CancellationToken cancellationToken = default)
    // {
    //      var productIds = itemDtos.Select(i => i.ProductId).Distinct().ToList();
    //     var products = await productRepository.GetByIdsAsync(productIds, cancellationToken);
    //     var productDict = products.ToDictionary(p => p.Id);
    //     
    //     var order = new Order(customerId);
    //     
    //     await using var transaction = await orderRepository.BeginTransactionAsync(cancellationToken);
    //     try
    //     {
    //         foreach (var itemDto in itemDtos)
    //         {
    //             if (!productDict.TryGetValue(itemDto.ProductId, out var product))
    //                 throw new EntityNotFoundException(nameof(Product), itemDto.ProductId);
    //     
    //             var success = await productRepository.TryDecreaseStockAsync(
    //                 itemDto.ProductId, itemDto.Quantity, cancellationToken);
    //     
    //             if (!success)
    //                 throw new InsufficientStockException(itemDto.ProductId, itemDto.Quantity, product.Stock);
    //     
    //             var item = new OrderItem(itemDto.ProductId, itemDto.Quantity, product.Price);
    //             order.InsertItem(item);
    //         }
    //     
    //         await orderRepository.AddAsync(order, cancellationToken);
    //         await orderRepository.SaveChangesAsync(cancellationToken);
    //     
    //         await transaction.CommitAsync(cancellationToken);
    //     }
    //     catch
    //     {
    //         await transaction.RollbackAsync(cancellationToken);
    //         throw;
    //     }
    //     
    //     return ToDto(order);
    // }

    public async Task RemoveOrders(long orderId, CancellationToken cancellationToken = default)
    {
        var order = await  orderRepository.GetOrderByIdAsync(orderId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Order), orderId);
        orderRepository.Remove(order, cancellationToken);
        await orderRepository.SaveChangesAsync(cancellationToken);
    }

    // public async Task<IReadOnlyList<OrderItemDto>> GetAllOrderItems(long orderId,CancellationToken cancellationToken = default)
    // {
    //     var order = await orderRepository.GetOrderByIdAsync(orderId, cancellationToken)
    //         ?? throw new EntityNotFoundException(nameof(Order), orderId);
    //
    //     var items = order.Items.ToList();
    //     
    //     return items.Select(ToItemDto).ToList();
    // }

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