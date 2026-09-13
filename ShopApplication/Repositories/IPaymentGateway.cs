using ShopApplication.DTOs.Payment;

namespace ShopApplication.Repositories;

public interface IPaymentGateway
{
    Task<PaymentRequestResult> RequestPaymentAsync(
        decimal amount,
        string callbackUrl,
        CancellationToken cancellationToken = default);

    Task<PaymentVerificationResult> VerifyPaymentAsync(
        string authority,
        decimal amount,
        CancellationToken cancellationToken = default);
}