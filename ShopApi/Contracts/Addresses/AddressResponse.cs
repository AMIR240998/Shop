namespace ShopApi.Contracts.Addresses;

public record AddressResponse(
        long Id,
        long CustomerId,
        string Province,
        string City,
        string AddressText,
        string PostalCode);