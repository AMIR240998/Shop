namespace ShopApi.Contracts.Customers;

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    DateTimeOffset CreateAt);
