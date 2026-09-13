namespace ShopApplication.DTOs.Address;

public record CreateAddressDto(
    long UserId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);
