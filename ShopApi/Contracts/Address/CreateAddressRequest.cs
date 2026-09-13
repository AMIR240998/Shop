namespace ShopApi.Contracts.Address;

public record CreateAddressRequest(
    long UserId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);