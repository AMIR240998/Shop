namespace ShopApplication.Repositories;

public interface IPasswordHasher
{
    string HashPassword(string password);
    
    bool VerifyHashedPassword(string password, string passwordHash);
}