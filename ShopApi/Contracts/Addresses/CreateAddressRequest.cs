namespace ShopApi.Contracts.Addresses;

public record CreateAddressRequest(
    long CustomerId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);