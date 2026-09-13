namespace ShopApplication.DTOs;

public record UserDto(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    string Role,
    DateTimeOffset CreateAt);