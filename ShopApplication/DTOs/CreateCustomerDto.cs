namespace ShopApplication.DTOs;

public record CreateCustomerDto(    
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    DateTimeOffset CreateAt);