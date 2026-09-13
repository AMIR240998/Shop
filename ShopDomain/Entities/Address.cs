using System.ComponentModel.Design;

namespace ShopDomain.Entities;

public class Address
{
    public long Id { get;private set; }

    public long UserId { get;private set; }

    public string Province { get; private set; }

    public string City { get; private set; } 

    public string AddressText { get;private set; }

    public string PostalCode { get;private set; }

    private Address()
    {
    }

    public Address(long userId,string province, string city, string addressText, string postalCode)
    {
        UserId = userId;
        AddressText = addressText;
        PostalCode = postalCode;
        Province = province;
        City = city;
    }

    public void Update(string province, string city, string addressText, string postalCode)
    {
        AddressText = addressText;
        PostalCode = postalCode;
        Province = province;
        City = city;
    }
}