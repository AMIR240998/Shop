using ShopApplication.DTOs;
using ShopApplication.Repositories;
using ShopDomain.Entities;
using ShopDomain.Enums;
using ShopDomain.Exceptions;

namespace ShopApplication.Services;

public class CustomerService(ICustomerRepository customerRepository,IPasswordHasher passwordHasher,
    ITokenService  tokenService,IAddressRepository addressRepository)
{
    public async Task<PagedResultDto<CustomerDto>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var (customers,totalCount) = await customerRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);
        var item = customers.Select(ToDto).ToList();
        return new PagedResultDto<CustomerDto>()
        {
            Items = item,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

    }

    public async Task<CustomerDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetByIdAsync(id, cancellationToken)
                       ?? throw new EntityNotFoundException(nameof(Customers), id);
        
        return  ToDto(customer);
    }

    public async Task<CustomerDto> AddAsync(CreateCustomerDto dto, CancellationToken cancellationToken = default)
    {
        var passwordHash = passwordHasher.HashPassword(dto.PasswordHash);
        var customer = new Customers(dto.FirstName, dto.LastName, dto.Email,passwordHash, dto.Address,dto.Username,UserRole.Customer);
        await customerRepository.AddAsync(customer, cancellationToken);
        await customerRepository.SaveChangesAsync(cancellationToken);
        return ToDto(customer);
    }

    public async Task<CustomerDto> UpdateAsync(long id, UpdateCustomerDto dto, CancellationToken cancellationToken = default)
    {
        var  customer = await customerRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Customers), id);

        customer.Update(dto.FirstName, dto.LastName, dto.Email, dto.PasswordHash, dto.Address, dto.Username);
        customerRepository.Update(customer);
        await customerRepository.SaveChangesAsync(cancellationToken);
        return ToDto(customer);
    }

    public async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Customers), id);
        customerRepository.Remove(customer);
        await customerRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsUserNameDuplicate(string userName)
    {
        var result = await customerRepository.IsUserNameDuplicate(userName);
        if(result)
            throw new DuplicateUserNameException(userName);
        return result;
    }


    public async Task<LoginResultDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetByUserName(dto.Username,cancellationToken)
                ?? throw new NotExistUserNameException(dto.Username);

        var passwordResult = passwordHasher.VerifyHashedPassword(dto.PasswordHash, customer.PasswordHash);
        if (!passwordResult)
            throw new InvalidPasswordException(dto.PasswordHash);
        
        var token = tokenService.GenerateJwtToken(customer);

        return new LoginResultDto(
            customer.Id,
            customer.FirstName,
            token);
    }
    private static CustomerDto ToDto(Customers customers) => new(
        customers.Id,
        customers.FirstName,
        customers.LastName,
        customers.Email,
        customers.UserName,
        customers.PasswordHash,
        customers.Address,
        customers.Role.ToString(),
        customers.CreateAt);
    
    
    
        public async Task<IReadOnlyList<AddressDto>> GetAllAddressAsync(CancellationToken cancellationToken = default)
    {
        var address = await addressRepository.GetAllAsync(cancellationToken);
        return address.Select(AddressMapToDto).ToList();
    }

    public async Task<AddressDto> GetByIdAddressAsync(long id, CancellationToken cancellationToken = default)
    {
        var address = await addressRepository.GetByIdAsync(id, cancellationToken)
                      ?? throw new EntityNotFoundException(nameof(Addresses), id);
        
        return AddressMapToDto(address);
    }

    public async Task<AddressDto> AddAddressAsync(CreateAddressDto dto, CancellationToken cancellationToken = default)
    {
        var address = new Addresses(dto.CustomerId,dto.Province,dto.City,dto.AddressText,dto.PostalCode);
        await addressRepository.AddAsync(address, cancellationToken);
        await addressRepository.SaveChangesAsync(cancellationToken);
        return AddressMapToDto(address);
    }

    public async Task<AddressDto> UpdateAddressAsync(long id, UpdateAddressDto dto, CancellationToken cancellationToken = default)
    {
        var  address = await addressRepository.GetByIdAsync(id, cancellationToken)
                        ?? throw new EntityNotFoundException(nameof(Addresses), id);
        
        
        address.Update(dto.Province, dto.City, dto.AddressText, dto.PostalCode);
        addressRepository.Update(address);
        await addressRepository.SaveChangesAsync(cancellationToken);
        return AddressMapToDto(address);
    }

    public async Task RemoveAddressAsync(long id, CancellationToken cancellationToken = default)
    {
        var address = await addressRepository.GetByIdAsync(id, cancellationToken)
                       ?? throw new EntityNotFoundException(nameof(Customers), id);
        
        addressRepository.Remove(address);
        await addressRepository.SaveChangesAsync(cancellationToken);
    }
    private static AddressDto AddressMapToDto(Addresses addresses) => new(
        addresses.Id,
        addresses.CustomerId,
        addresses.Province,
        addresses.City,
        addresses.AddressText,
        addresses.PostalCode);
}