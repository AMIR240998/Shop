using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class Payments
{
    public long Id { get;private set; }

    public long OrderId { get;private set; }

    public decimal Amount { get;private set; }

    public DateTimeOffset? PaymentDate { get;private set; }

    public int? TrackingCode { get;private set; }
    
    public PaymentStatus Status { get;private set; }


    private Payments()
    {
    }
    
    public Payments(long id, long orderId, decimal amount, int? trackingCode, PaymentStatus status)
    {
        Id = id;
        OrderId = orderId;
        Amount = amount;
        PaymentDate = DateTimeOffset.UtcNow;
        TrackingCode = trackingCode;
        Status = status;
    }
}