using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class Payment
{
    public long Id { get;private set; }

    public long OrderId { get;private set; }

    public decimal Amount { get;private set; }

    public DateTimeOffset? PaymentDate { get;private set; }

    public string? TrackingCode { get;private set; }
    
    public string Status { get;private set; }


    private Payment()
    {
    }
    
    public Payment(long id, long orderId, decimal amount, string? trackingCode, PaymentStatus status)
    {
        Id = id;
        OrderId = orderId;
        Amount = amount;
        PaymentDate = DateTimeOffset.UtcNow;
        TrackingCode = trackingCode;
        Status = status.ToString();
    }
    public void MarkAsSuccessful(string trackingCode)
    {
        Status = nameof(PaymentStatus.Success);
        TrackingCode = trackingCode;
        PaymentDate = DateTimeOffset.UtcNow;
    }
}