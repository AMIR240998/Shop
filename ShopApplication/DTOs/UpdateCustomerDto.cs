using ShopDomain.Enums;

namespace ShopApplication.DTOs;

public record UpdateCustomerDto(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address);
