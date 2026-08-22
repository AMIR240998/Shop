namespace ShopApi.Contracts.Login;

public record LoginResponse(
    long Id,
    string Username,
    string Token);
