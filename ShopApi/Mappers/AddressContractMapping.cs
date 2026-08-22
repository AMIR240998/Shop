using ShopApi.Contracts.Addresses;
using ShopApplication.DTOs;

namespace ShopApi.Mappers;

public static class AddressContractMapping
{
    public static AddressResponse MapToResponse(this AddressDto dto) => new(
        dto.Id,
        dto.CustomerId,
        dto.Province,
        dto.City,
        dto.AddressText,
        dto.PostalCode);
    
    
    public static CreateAddressDto CreateMapToDto(this CreateAddressRequest request) => new (
        request.CustomerId,
        request.Province,
        request.City,
        request.AddressText,
        request.PostalCode);
}