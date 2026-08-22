namespace ShopDomain.Entities;

public class Discounts
{
    public long Id { get;private set; }

    public string Code { get;private set; }

    public short Percent { get;private set; }

    public DateTimeOffset ExpireDate { get;private set; }

    public short MaxUse { get;private set; }

    public bool IsActive { get;private set; }
    
    
    
    private Discounts()
    {
    }

    public Discounts(long id, string code, short percent, DateTimeOffset expireDate)
    {
        Id = id;
        Code = code;
        Percent = percent;
        ExpireDate = expireDate;
        MaxUse = 1;
        IsActive = true;
    }
}