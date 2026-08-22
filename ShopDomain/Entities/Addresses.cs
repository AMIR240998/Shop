using System.ComponentModel.Design;

namespace ShopDomain.Entities;

public class Addresses
{
    public long Id { get;private set; }

    public long CustomerId { get;private set; }

    public string Province { get; private set; }

    public string City { get; private set; } 

    public string AddressText { get;private set; }

    public string PostalCode { get;private set; }

    private Addresses()
    {
    }

    public Addresses(long customerId,string province, string city, string addressText, string postalCode)
    {
        CustomerId = customerId;
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