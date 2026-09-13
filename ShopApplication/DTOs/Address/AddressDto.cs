namespace ShopApplication.DTOs.Address;

public record AddressDto(
    long Id,
    long UserId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);