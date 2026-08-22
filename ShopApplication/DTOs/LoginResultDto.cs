namespace ShopApplication.DTOs;

public record LoginResultDto(
    long Id,
    string Username,
    string Token);