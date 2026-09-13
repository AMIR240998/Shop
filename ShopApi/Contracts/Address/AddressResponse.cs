namespace ShopApi.Contracts.Address;

public record AddressResponse(
        long Id,
        long UserId,
        string Province,
        string City,
        string AddressText,
        string PostalCode);