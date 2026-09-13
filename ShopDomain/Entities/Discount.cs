using System.Security.Cryptography;
using ShopDomain.Constants;

namespace ShopDomain.Entities;

public class Discount
{
    public long Id { get;private set; }

    public string Code { get;private set; }

    public decimal Percent { get;private set; }

    public DateTimeOffset ExpireDate { get;private set; }

    public short MaxUse { get;private set; }

    public bool IsActive { get;private set; }
    
    
    
    private Discount()
    {
    }

    public Discount(string? code, decimal percent, DateTimeOffset expireDate, short maxUse = 1)
    {
        if (percent is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be between 1 and 100.");

        Code = string.IsNullOrWhiteSpace(code)
            ? GenerateCode()
            : code.Trim().ToUpperInvariant();

        Percent = percent;
        ExpireDate = expireDate;
        MaxUse = maxUse;
        IsActive = true;
    }

    private static string GenerateCode(int length = 10)
    {
        const string chars = DiscountConstant.Chars;
        var bytes = RandomNumberGenerator.GetBytes(length);
        return new string(bytes.Select(b => chars[b % chars.Length]).ToArray());
    }
}