using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Addresses;
using ShopApi.Contracts.Customers;
using ShopApi.Contracts.Login;
using ShopApi.Contracts.Pagination;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;
[ApiVersion(1.0)]
public class CustomerController(CustomerService customerService) : BaseController
{
    [HttpGet(CustomersUrlConstants.GetAll)]
    public async Task<ApiResult<PagedResult<CustomerResponse>>> GetAll(
        [FromQuery] PaginationRequest request,
        CancellationToken cancellationToken = default)
    {
        var customers = await customerService.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        var response = new PagedResult<CustomerResponse>
        {
            Items = customers.Items.Select(x => x.MapToResponse()).ToList(),
            PageNumber = customers.PageNumber,
            PageSize = customers.PageSize,
            TotalCount = customers.TotalCount
        };
        return response;
    }

    [HttpGet(CustomersUrlConstants.GetById)]
    public async Task<ApiResult<CustomerResponse>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var customer = await customerService.GetByIdAsync(id, cancellationToken);
        return customer.MapToResponse();
    }

    [HttpPost(CustomersUrlConstants.Add)]
    public async Task<ApiResult<CustomerResponse>> Add(CreateCustomerRequest request,CancellationToken cancellationToken = default)
    {
        var customer = await customerService.AddAsync(request.MapToCreateDto(), cancellationToken);
        return customer.MapToResponse();
    }

    [HttpPut(CustomersUrlConstants.Update)]
    public async Task<ApiResult<CustomerResponse>> Update(long id, UpdateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var customer = await customerService.UpdateAsync(id, request.MapToUpdateDto(), cancellationToken);
        return customer.MapToResponse();
    }

    [HttpDelete(CustomersUrlConstants.Remove)]
    public async Task<ApiResult> Remove(long id,CancellationToken cancellationToken = default)
    {
        await customerService.RemoveAsync(id, cancellationToken);
        return ApiResult.NoContent();
    }

    [HttpGet(CustomersUrlConstants.Exist)]
    public async Task<ActionResult<bool>> IsUserNameDuplicate(string userName)
    {
        return await customerService.IsUserNameDuplicate(userName);
    }

    [HttpPost(CustomersUrlConstants.Login)]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request,CancellationToken cancellationToken = default)
    {
        var login = await customerService.LoginAsync(request.MapToDto(), cancellationToken);
        return Ok(login.MapToResponse());
    }


    [HttpGet(AddressUrlConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<AddressResponse>>> GetAllAddress(
        CancellationToken cancellationToken = default)
    {
        var addresses = await customerService.GetAllAddressAsync(cancellationToken);
        return addresses.Select(a => a.MapToResponse()).ToList();
    }

    [HttpGet(AddressUrlConstants.GetById)]
    public async Task<ApiResult<AddressResponse>> GetByIdAddress(long id, CancellationToken cancellationToken = default)
    {
        var address = await customerService.GetByIdAddressAsync(id, cancellationToken);
        return address.MapToResponse();
    }
    [HttpPost(AddressUrlConstants.Add)]
    public async Task<ApiResult<AddressResponse>> AddAddress(CreateAddressRequest request ,
        CancellationToken cancellationToken = default)
    {
        var address = await customerService.AddAddressAsync(request.CreateMapToDto(), cancellationToken);
        return address.MapToResponse();
    }
}