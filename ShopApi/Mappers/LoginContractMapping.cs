using Microsoft.AspNetCore.Identity.Data;
using ShopApi.Contracts.Login;
using ShopApplication.DTOs;
using LoginRequest = ShopApi.Contracts.Login.LoginRequest;

namespace ShopApi.Mappers;

public static class LoginContractMapping
{

    public static LoginResponse MapToResponse(this LoginResultDto dto) => new(
        dto.Id,
        dto.Username,
        dto.Token);
    public static LoginDto MapToDto(this LoginRequest request) => new(
        request.Username,
        request.PasswordHash);
}