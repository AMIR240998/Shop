namespace ShopApplication.Services.Interface;

public interface ICheckoutService
{
    Task CheckoutAsync(long basketId, CancellationToken cancellationToken = default);
}