namespace ShopApplication.DTOs;

public record AddressDto(
    long Id,
    long CustomerId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);