namespace ShopDomain.Exceptions;

public class InsufficientStockException(long? productId, int? requested, int? available) :
    Exception($"Insufficient stock for product {productId}. Requested: {requested}, Available: {available}")
{
}