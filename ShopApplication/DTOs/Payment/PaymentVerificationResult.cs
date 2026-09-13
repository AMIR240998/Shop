namespace ShopApplication.DTOs.Payment;

public class PaymentVerificationResult
{
    public bool IsSuccess { get; set; }

    public string? TrackingCode { get; set; }

    public string? ErrorMessage { get; set; }
}