namespace ShopApi.Contracts.Customers;

public record UpdateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address);