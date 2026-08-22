namespace ShopApi.Contracts.Customers;

public record CustomerResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    string Role,
    DateTimeOffset CreateAt);
