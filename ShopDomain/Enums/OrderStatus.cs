namespace ShopDomain.Enums;

public enum OrderStatus
{
    Pending = 1, //درحال تحویل
    Shipped = 2, //ارسال شده
    Delivered = 3, //تحویل داده شده
    Cancelled = 4, //لغو شده
    Paid = 5, //پرداخت شده
    Preparing = 6, // درحال آماده سازی
}