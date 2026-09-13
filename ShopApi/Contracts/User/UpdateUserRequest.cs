namespace ShopApi.Contracts.Customers;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address);