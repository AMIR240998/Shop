namespace ShopApplication.DTOs;

public record CreateAddressDto(
    long CustomerId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);
