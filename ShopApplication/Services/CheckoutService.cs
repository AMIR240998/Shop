using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class CheckoutService(
    IProductRepository productRepository,
    IOrderRepository orderRepository,
    IBasketRepository basketRepository,
    IUnitOfWork unitOfWork)
{
    public async Task CheckoutAsync(long basketId, CancellationToken cancellationToken = default)
    {
           await using var tran = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var basket = await basketRepository.GetBasketByIdAsync(basketId, cancellationToken)
                         ?? throw new EntityNotFoundException(nameof(Basket), basketId);

            var basketItems = basket.Items.ToList();

            if (basketItems.Count <= 0)
                throw new InvalidQuantityException(nameof(basketItems));

            var order = new Order(basket.UserId);
            await orderRepository.AddAsync(order, cancellationToken);

            var productIds = basketItems.Select(p => p.ProductId).Distinct().ToList();
            var products = await productRepository.GetByIdsAsync(productIds, cancellationToken);
            var productDict = products.ToDictionary(p => p.Id);

            foreach (var basketItem in basketItems)
            {
                if(!productDict.TryGetValue(basketItem.ProductId, out var product))
                    throw new EntityNotFoundException(nameof(Product), basketItem.ProductId);
                
                product.DecreaseStock(basketItem.Quantity);

                var item = new OrderItem(basketItem.ProductId, basketItem.Quantity, product.Price);

                order.InsertItem(item);
            }

            basket.ClearItems();
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await tran.CommitAsync(cancellationToken);
        }
        catch
        {
            await tran.RollbackAsync(cancellationToken);
            throw;
        }
    }
}