using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class User
{
    private readonly List<Address> _addresses = [];
    
    public long Id { get;private set; }

    public string FirstName { get;private set; }

    public string LastName { get;private set; }

    public string Email { get;private set; }
    
    public string UserName { get;private set; }

    public string PasswordHash { get;private set; }

    public string Address { get;private set; }

    public UserRole Role { get; private set; }
    
    public DateTimeOffset CreatedAt { get;private set; }
    
    public Basket? Basket { get;private set; }
    
    public Wallet? Wallet { get;private set; }
    
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    


    private User()
    {
    }
    
    public User(string firstName, string lastName, string email, string passwordHash, string address,
        string userName,UserRole role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Address = address;
        CreatedAt = DateTimeOffset.UtcNow;
        UserName = userName;
        Role = role;
    }

    public void Update(string firstName, string lastName, string email, string passwordHash, string address,
        string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Address = address;
        UserName = userName;
    }

    public void SetBasket(Basket basket)
    {
        Basket = basket;
    }

    public void SetWallet(Wallet wallet)
    {
        Wallet = wallet;
    }

    public void AddAddress(Address address)
    {
        _addresses.Add(address);
    }

    public void RemoveAddress(Address address)
    {
        _addresses.Remove(address);
    }
}