using ShopApplication.DTOs.Payment;
using ShopApplication.Repositories;

namespace ShopInfrastructure.services.Payment;

public class FakePaymentGateway : IPaymentGateway
{
    public Task<PaymentRequestResult> RequestPaymentAsync(
        decimal amount,
        string callbackUrl,
        CancellationToken cancellationToken = default)
    {
        var authority = Guid.NewGuid().ToString();

        var result = new PaymentRequestResult
        {
            IsSuccess = true,
            Authority = authority,
            PaymentUrl =
                $"https://localhost:5001/payment/fake?authority={authority}"
        };

        return Task.FromResult(result);
    }

    public Task<PaymentVerificationResult> VerifyPaymentAsync(
        string authority,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var result = new PaymentVerificationResult
        {
            IsSuccess = true,
            TrackingCode = Random.Shared
                .Next(100000000, 999999999)
                .ToString()
        };

        return Task.FromResult(result);
    }
}