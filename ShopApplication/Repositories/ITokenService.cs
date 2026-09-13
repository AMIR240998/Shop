using ShopDomain.Entities;

namespace ShopApplication.Repositories;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}