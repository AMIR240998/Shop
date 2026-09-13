using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Constants;
using ShopApi.Contracts;
using ShopApi.Contracts.Address;
using ShopApi.Contracts.Customers;
using ShopApi.Contracts.Login;
using ShopApi.Contracts.Pagination;
using ShopApi.Contracts.User;
using ShopApi.Mappers;
using ShopApplication.Services;

namespace ShopApi.Controllers.V1;
[ApiVersion(1.0)]
public class UserController(UserService userService) : BaseController
{
    [HttpGet(UserUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<UserResponse>>> GetAll(
        CancellationToken cancellationToken = default)
    {
        var user = await userService.GetAllAsync(cancellationToken);
        return user.Select(c => c.MapToResponse()).ToList();
    }

    [HttpGet(UserUrlConstant.GetById)]
    public async Task<ApiResult<UserResponse>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(id, cancellationToken);
        return user.MapToResponse();
    }

    [HttpPost(UserUrlConstant.Add)]
    public async Task<ApiResult<UserResponse>> Add(CreateUserRequest request,CancellationToken cancellationToken = default)
    {
        var user = await userService.AddAsync(request.MapToCreateDto(), cancellationToken);
        return user.MapToResponse();
    }

    [HttpPut(UserUrlConstant.Update)]
    public async Task<ApiResult<UserResponse>> Update(long id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userService.UpdateAsync(id, request.MapToUpdateDto(), cancellationToken);
        return user.MapToResponse();
    }

    [HttpDelete(UserUrlConstant.Remove)]
    public async Task<ApiResult> Remove(long id,CancellationToken cancellationToken = default)
    {
        await userService.RemoveAsync(id, cancellationToken);
        return ApiResult.NoContent();
    }

    [HttpGet(UserUrlConstant.Exist)]
    public async Task<ActionResult<bool>> IsUserNameDuplicate(string userName)
    {
        return await userService.IsUserNameDuplicate(userName);
    }

    [HttpPost(UserUrlConstant.Login)]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request,CancellationToken cancellationToken = default)
    {
        var login = await userService.LoginAsync(request.MapToDto(), cancellationToken);
        return Ok(login.MapToResponse());
    }


    [HttpGet(AddressUrlConstant.GetAll)]
    public async Task<ApiResult<IReadOnlyList<AddressResponse>>> GetAllAddress(long userId,
        CancellationToken cancellationToken = default)
    {
        var address = await userService.GetAllAddressAsync(userId,cancellationToken);
        return address.Select(a => a.MapToResponse()).ToList();
    }

    [HttpGet(AddressUrlConstant.GetById)]
    public async Task<ApiResult<AddressResponse>> GetByIdAddress(long userId,long addressId,
        CancellationToken cancellationToken = default)
    {
        var address = await userService.GetByIdAddressAsync(userId, addressId, cancellationToken);
        return address.MapToResponse();
    }
    [HttpPost(AddressUrlConstant.Add)]
    public async Task<ApiResult<AddressResponse>> AddAddress(CreateAddressRequest request ,
        CancellationToken cancellationToken = default)
    {
        var address = await userService.AddAddressAsync(request.CreateMapToDto(), cancellationToken);
        return address.MapToResponse();
    }

    [HttpPut(AddressUrlConstant.Update)]
    public async Task<ApiResult<AddressResponse>> UpdateAddress(long id,UpdateAddressRequest request,
        CancellationToken cancellationToken = default)
    {
        var address = await userService.UpdateAddressAsync(id,request.UpdateMapToDto(), cancellationToken);
        return address.MapToResponse();
    }

    [HttpDelete(AddressUrlConstant.Remove)]
    public async Task<ApiResult> RemoveAddress(long userId,long addressId,
        CancellationToken cancellationToken = default)
    {
        await  userService.RemoveAddressAsync(userId, addressId, cancellationToken);
        return ApiResult.Secceded();
    }
}