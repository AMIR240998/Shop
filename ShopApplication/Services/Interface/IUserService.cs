using ShopApplication.DTOs;
using ShopApplication.DTOs.Address;
using ShopApplication.DTOs.User;

namespace ShopApplication.Services.Interface;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<UserDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);


    Task<UserDto> AddAsync(CreateUserDto dto, CancellationToken cancellationToken = default);


    Task<UserDto> UpdateAsync(long id, UpdateUserDto dto, CancellationToken cancellationToken = default);


    Task RemoveAsync(long id, CancellationToken cancellationToken = default);


    Task<bool> IsUserNameDuplicate(string userName);

    Task<LoginResultDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<AddressDto>> GetAllAddressAsync(long userId,CancellationToken cancellationToken = default);


    Task<AddressDto> GetByIdAddressAsync(long userId,long addressId , CancellationToken cancellationToken = default);
  

    Task<AddressDto> AddAddressAsync(CreateAddressDto dto, CancellationToken cancellationToken = default);


    Task<AddressDto> UpdateAddressAsync(long id, UpdateAddressDto dto, CancellationToken cancellationToken = default);


    Task RemoveAddressAsync(long userId,long addressId,CancellationToken cancellationToken = default);


}