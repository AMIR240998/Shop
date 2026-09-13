using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class WalletTransaction
{
    public long Id { get;private set; }
    public long WalletId { get;private set; }
    public decimal Amount { get;private set; }
    public string Type { get;private set; }
    public string Status {get; private set; }
    public string? Description { get;private set; }
    public string? RefrenceId { get;private set; }
    public DateTimeOffset CreateAt { get;private set; }

    private WalletTransaction()
    {
    }

    public WalletTransaction(long walletId, decimal amount, WalletTransactionType type)
    {
        WalletId = walletId;
        Amount = amount;
        Type = type.ToString();
        Status = WalletTransactionStatus.Pending.ToString();
        CreateAt = DateTimeOffset.UtcNow;
    }

    public void SetRefrenceId(string refrenceId)
    {
        RefrenceId = refrenceId;
    }

    public void AddDescription(string description)
    {
        Description = description;
    }
}