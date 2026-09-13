namespace ShopApplication.DTOs.Payment;

public class PaymentRequestResult
{
    public bool IsSuccess { get; set; }

    public string? Authority { get; set; }

    public string? PaymentUrl { get; set; }

    public string? ErrorMessage { get; set; }
}