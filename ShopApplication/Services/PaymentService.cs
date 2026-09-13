using ShopApplication.DTOs.Payment;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopApplication.Services;

public class PaymentService(IPaymentGateway paymentGateway, IPaymentRepository paymentRepository)
{
    
    public async Task<PaymentRequestResult> RequestPaymentAsync(
    Payment payment,
    string callbackUrl,
        CancellationToken cancellationToken = default)
    {
        var result = await paymentGateway.RequestPaymentAsync(
            payment.Amount,
            callbackUrl,
            cancellationToken);

        if (!result.IsSuccess)
            return result;

        return result;
    }

    public async Task<PaymentVerificationResult> VerifyPaymentAsync(
        long orderId,
        string authority,
        CancellationToken cancellationToken = default)
    {
        var payment = await paymentRepository
            .GetByOrderIdAsync(orderId, cancellationToken);

        if (payment is null)
            throw new Exception("Payment not found.");

        var result = await paymentGateway.VerifyPaymentAsync(
            authority,
            payment.Amount,
            cancellationToken);

        if (result.IsSuccess)
        {
            payment.MarkAsSuccessful(result.TrackingCode);

            await paymentRepository
                .SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}