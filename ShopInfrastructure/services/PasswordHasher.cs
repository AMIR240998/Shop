using Microsoft.AspNetCore.Identity;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopInfrastructure.services;

public class PasswordHasher : IPasswordHasher
{
    private readonly Microsoft.AspNetCore.Identity.PasswordHasher<User> _hasher = new();
    
    
    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    public bool VerifyHashedPassword(string password, string passwordHash)
    {
        var result = _hasher.VerifyHashedPassword(
            null!,
            passwordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}