using ShopApi.Contracts.Order;
using ShopApi.Contracts.Orders;
using ShopApplication.DTOs.Order;

namespace ShopApi.Mappers;

public static class OrderContractMapping
{
    public static OrderResponse ToResponse(this OrderDto dto) => new(
        dto.Id,
        dto.UserId,
        dto.OrderDate,
        dto.OrderStatus,
        dto.TotalPrice);

    public static OrderItemResponse ToItemResponse(this OrderItemDto dto) => new(
        dto.Id,
        dto.OrderId,
        dto.ProductId,
        dto.Count,
        dto.Price);
    
    public static List<CreateOrderItemDto> ToDto(
        this List<CreateOrderItemRequest> request)
    {
        return request.Select(o => new CreateOrderItemDto(
            o.ProductId,
            o.Count
        )).ToList();
    }
}