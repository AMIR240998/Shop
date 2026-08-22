namespace ShopApplication.DTOs;

public record UpdateAddressDto(
    string Province,
    string City,
    string AddressText,
    string PostalCode);