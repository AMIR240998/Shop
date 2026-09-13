namespace ShopApplication.DTOs;

public record CreateUserDto(    
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    DateTimeOffset CreateAt);