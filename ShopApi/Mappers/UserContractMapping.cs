using ShopApi.Contracts.Customers;
using ShopApi.Contracts.User;
using ShopApplication.DTOs;
using ShopApplication.DTOs.User;

namespace ShopApi.Mappers;

public static class UserContractMapping
{
    public static UserResponse MapToResponse(this UserDto userDto) => new(
        userDto.Id,
        userDto.FirstName,
        userDto.LastName,
        userDto.Email,
        userDto.Username,
        userDto.PasswordHash,
        userDto.Address,
        userDto.Role,
        userDto.CreateAt);

    public static CreateUserDto MapToCreateDto(this CreateUserRequest request) => new(
        request.FirstName,
        request.LastName,
        request.Email,
        request.Username,
        request.PasswordHash,
        request.Address,
        request.CreateAt);

    public static UpdateUserDto MapToUpdateDto(this UpdateUserRequest request) => new(
        request.FirstName,
        request.LastName,
        request.Email,
        request.Username,
        request.PasswordHash,
        request.Address);

}