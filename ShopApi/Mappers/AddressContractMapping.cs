using ShopApi.Contracts.Address;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Address;

namespace ShopApi.Mappers;

public static class AddressContractMapping
{
    public static AddressResponse MapToResponse(this AddressDto dto) => new(
        dto.Id,
        dto.UserId,
        dto.Province,
        dto.City,
        dto.AddressText,
        dto.PostalCode);
    
    
    public static CreateAddressDto CreateMapToDto(this CreateAddressRequest request) => new (
        request.UserId,
        request.Province,
        request.City,
        request.AddressText,
        request.PostalCode);

    public static UpdateAddressDto UpdateMapToDto(this UpdateAddressRequest request) => new(
        request.UserId,
        request.Province,
        request.City,
        request.AddressText,
        request.PostalCode);
}