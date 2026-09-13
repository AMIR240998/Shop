namespace ShopApplication.DTOs.Address;

public record UpdateAddressDto(
    long UserId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);