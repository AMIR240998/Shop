namespace ShopApi.Contracts.Login;

public record LoginRequest(
    string Username,
    string PasswordHash);
