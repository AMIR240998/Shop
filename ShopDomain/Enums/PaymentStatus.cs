namespace ShopDomain.Enums;

public enum PaymentStatus
{
    Pending = 1, // در انتظار پرداخت
    Success = 2, // موفق
    Failed = 3, // ناموفق
    Refunded = 4, // بازگشت وجه
}