namespace ShopApi.Contracts.Address;

public record UpdateAddressRequest(
    long UserId,
    string Province,
    string City,
    string AddressText,
    string PostalCode);