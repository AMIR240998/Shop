using ShopApplication.DTOs;
using ShopApplication.DTOs.Address;
using ShopApplication.DTOs.User;
using ShopApplication.Repositories;
using ShopApplication.Services.Interface;
using ShopDomain.Entities;
using ShopDomain.Enums;
using ShopDomain.Exceptions;

namespace ShopApplication.Services.Implementation;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService) : IUserService
{
    public async Task<IReadOnlyList<UserDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAllAsync(cancellationToken);
        return user.Select(ToDto).ToList();
    }

    public async Task<UserDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken)
                   ?? throw new EntityNotFoundException(nameof(User), id);
        
        return  ToDto(user);
    }

    public async Task<UserDto> AddAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
            await using var tran = await userRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var passwordHash = passwordHasher.HashPassword(dto.PasswordHash);
                var user = new User(dto.FirstName, dto.LastName, dto.Email,passwordHash, dto.Address,dto.Username,UserRole.Customer);
                
                await userRepository.AddAsync(user, cancellationToken);
                
                var basket = new Basket();
                var wallet = new Wallet();
                
                user.SetBasket(basket);
                user.SetWallet(wallet);
                
                await userRepository.SaveChangesAsync(cancellationToken);
                
                await tran.CommitAsync(cancellationToken);
                
                return ToDto(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.InnerException?.InnerException?.Message);
                await  tran.RollbackAsync(cancellationToken);
                throw;
            }
    }

    public async Task<UserDto> UpdateAsync(long id, UpdateUserDto dto, CancellationToken cancellationToken = default)
    {
        var  user = await userRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new EntityNotFoundException(nameof(User), id);
        var passwordHash = passwordHasher.HashPassword(user.PasswordHash);
        user.Update(dto.FirstName, dto.LastName, dto.Email, passwordHash, dto.Address, dto.Username);
        userRepository.Update(user);
        await userRepository.SaveChangesAsync(cancellationToken);
        return ToDto(user);
    }

    public async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken)
                   ?? throw new EntityNotFoundException(nameof(User), id);
        userRepository.Remove(user);
        await userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsUserNameDuplicate(string userName)
    {
        var result = await userRepository.IsUserNameDuplicate(userName);
        if(result)
            throw new DuplicateUserNameException(userName);
        return result;
    }


    public async Task<LoginResultDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByUserName(dto.Username,cancellationToken)
                   ?? throw new NotExistUserNameException(dto.Username);

        var passwordResult = passwordHasher.VerifyHashedPassword(dto.PasswordHash, user.PasswordHash);
        if (!passwordResult)
            throw new InvalidPasswordException(dto.PasswordHash);
        
        var token = tokenService.GenerateJwtToken(user);
        
        return new LoginResultDto(
            user.Id,
            user.FirstName,
            token);
    }
    private static UserDto ToDto(User user) => new(
        user.Id,
        user.FirstName,
        user.LastName,
        user.Email,
        user.UserName,
        user.PasswordHash,
        user.Address,
        user.Role.ToString(),
        user.CreatedAt);
    
    
    
        public async Task<IReadOnlyList<AddressDto>> GetAllAddressAsync(long userId,CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(User), userId);
        
        var addresses = user.Addresses.ToList();
        
        return addresses.Select(AddressMapToDto).ToList();
    }

    public async Task<AddressDto> GetByIdAddressAsync(long userId,long addressId ,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(User), userId);
        
        var address = user.Addresses.FirstOrDefault(a => a.Id == addressId)
            ?? throw new EntityNotFoundException(nameof(Address), addressId);
        
        return AddressMapToDto(address);
    }

    public async Task<AddressDto> AddAddressAsync(CreateAddressDto dto, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(dto.UserId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(User), dto.UserId);
        
        var address = new Address(dto.UserId,dto.Province,dto.City,dto.AddressText,dto.PostalCode);
        user.AddAddress(address);
        
        await userRepository.SaveChangesAsync(cancellationToken);
        
        return AddressMapToDto(address);
    }

    public async Task<AddressDto> UpdateAddressAsync(long id, UpdateAddressDto dto, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(dto.UserId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(User), dto.UserId);
        
        var address = user.Addresses.FirstOrDefault(a => a.Id == id)
            ?? throw new EntityNotFoundException(nameof(Address), id);
        
        address.Update(dto.Province, dto.City, dto.AddressText, dto.PostalCode);
        
        await userRepository.SaveChangesAsync(cancellationToken);
        
        return AddressMapToDto(address);
    }

    public async Task RemoveAddressAsync(long userId,long addressId,CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(User), userId);
        
        var address = user.Addresses.FirstOrDefault(a => a.Id == addressId)
            ?? throw new EntityNotFoundException(nameof(Address), addressId);
                
        user.RemoveAddress(address);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
    
    private static AddressDto AddressMapToDto(Address address) => new(
        address.Id,
        address.UserId,
        address.Province,
        address.City,
        address.AddressText,
        address.PostalCode);
}