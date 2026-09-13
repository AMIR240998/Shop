namespace ShopApi.Contracts.Customers;

public record UserResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    string Role,
    DateTimeOffset CreateAt);
