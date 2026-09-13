namespace ShopDomain.Enums;

public enum WalletTransactionType
{
    Deposit = 1, //شارژ کیف پول
    Withdrawal, //برداشت
    Purchase, // خرج کردن برای خرید
    Refund //برگشت وجه
}