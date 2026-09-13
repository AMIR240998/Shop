namespace ShopApplication.DTOs;

public record LoginDto(
    string Username,
    string PasswordHash);
