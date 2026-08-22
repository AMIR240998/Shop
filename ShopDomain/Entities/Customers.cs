using ShopDomain.Enums;

namespace ShopDomain.Entities;

public class Customers
{
    public long Id { get;private set; }

    public string FirstName { get;private set; }

    public string LastName { get;private set; }

    public string Email { get;private set; }
    
    public string UserName { get;private set; }

    public string PasswordHash { get;private set; }

    public string Address { get;private set; }

    public UserRole Role { get; private set; }
    
    public DateTimeOffset CreateAt { get;private set; }


    private Customers()
    {
    }
    
    public Customers(string firstName, string lastName, string email, string passwordHash, string address,string userName,UserRole role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Address = address;
        CreateAt = DateTime.UtcNow;
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
}