using ShopDomain.Exceptions;

namespace ShopDomain.Entities;

public class Wallet
{
    private readonly List<WalletTransaction> _walletTransactions = [];
    public long Id { get;private set; }
    
    public long UserId { get;private set; }
    
    public decimal Balance { get;private set; }
    
    public bool IsActive { get;private set; }
    
    public DateTimeOffset CreateAt { get;private set; }
    
    public DateTimeOffset? UpdateAt { get;private set; }

    public IReadOnlyCollection<WalletTransaction> WalletTransactions => _walletTransactions.AsReadOnly();

    
    // private Wallet()
    // {
    // }

    public Wallet()
    {
        Balance = 0;
        IsActive = true;
        CreateAt = DateTimeOffset.UtcNow;
    }

    public void UpdateDateTime()
    {
        UpdateAt = DateTimeOffset.UtcNow;
    }

    public void UpdateBalance(decimal balance)
    {
        if (balance <= 0)
            throw new InvalidBalanceException();
        Balance = balance;
    }
    public void AddTransaction(WalletTransaction transaction)
    {
        _walletTransactions.Add(transaction);
    }
}