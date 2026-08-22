using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Contracts;

public class ApiResult(
    bool isValid,
    string message,
    int status) : IActionResult
{
    public bool IsValid { get;} = isValid;
    public string Message { get;} = message;
    public int Status { get;} = status;
    
    public static ApiResult Secceded(int status = StatusCodes.Status200OK)
        => new(true, string.Empty, status);
    
    public static ApiResult Failed(string? message, int status)
        => new(false, message, status);
    
    public static ApiResult NoContent()
        => new(true, string.Empty, StatusCodes.Status204NoContent);
    
    
    public Task ExecuteResultAsync(ActionContext context)
    {
        return new ObjectResult(this)
        {
            StatusCode = Status
        }.ExecuteResultAsync(context);
    }
}
public sealed class ApiResult<T>(
    T? data,
    bool isValid,
    string message,
    int status) : IActionResult
{
    public T? Data { get; set; } =  data;
    public bool IsValid { get;} = isValid;
    public string Message { get;} = message;
    public int Status { get;} = status;
    
    
    public static ApiResult<T> Secceded(T? data ,int status = StatusCodes.Status200OK)
        => new(data ,true, string.Empty, status);
    
    public static ApiResult<T> Failed(string message, int status)
        => new(default,false, message, status);
    
    public static ApiResult<T> NoContent()
        => new(default,true, string.Empty, StatusCodes.Status204NoContent);
    
    public static ApiResult<T> Created(T? data, int status = StatusCodes.Status201Created)
    => new(data, true, string.Empty, status);
    
    public static implicit operator ApiResult<T> (T data) => Secceded(data);
    
    
    public Task ExecuteResultAsync(ActionContext context)
    {
        return new ObjectResult(this)
        {
            StatusCode = Status
        }.ExecuteResultAsync(context);
    }
}