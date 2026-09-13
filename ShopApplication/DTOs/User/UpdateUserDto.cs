namespace ShopApplication.DTOs.User;

public record UpdateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address);
