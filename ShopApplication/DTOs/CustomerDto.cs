namespace ShopApplication.DTOs;

public record CustomerDto(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    string Role,
    DateTimeOffset CreateAt);