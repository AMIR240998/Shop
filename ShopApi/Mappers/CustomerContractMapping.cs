using ShopApi.Contracts.Customers;
using ShopApplication.DTOs;

namespace ShopApi.Mappers;

public static class CustomerContractMapping
{
    public static CustomerResponse MapToResponse(this CustomerDto customerDto) => new(
        customerDto.Id,
        customerDto.FirstName,
        customerDto.LastName,
        customerDto.Email,
        customerDto.Username,
        customerDto.PasswordHash,
        customerDto.Address,
        customerDto.Role,
        customerDto.CreateAt);

    public static CreateCustomerDto MapToCreateDto(this CreateCustomerRequest request) => new(
        request.FirstName,
        request.LastName,
        request.Email,
        request.Username,
        request.PasswordHash,
        request.Address,
        request.CreateAt);

    public static UpdateCustomerDto MapToUpdateDto(this UpdateCustomerRequest request) => new(
        request.FirstName,
        request.LastName,
        request.Email,
        request.Username,
        request.PasswordHash,
        request.Address);

}