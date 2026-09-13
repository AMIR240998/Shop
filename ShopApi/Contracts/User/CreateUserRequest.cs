namespace ShopApi.Contracts.User;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PasswordHash,
    string Address,
    DateTimeOffset CreateAt);
