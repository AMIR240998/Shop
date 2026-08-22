using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopApi.Filters;

public class ValidationFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var errors = new Dictionary<string, String[]>();
        
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            if (!context.ActionArguments.TryGetValue(parameter.Name, out var argument) || argument == null)
                continue;
            
            var validator = serviceProvider.GetService(typeof(IValidator<>)
                .MakeGenericType(argument.GetType())) as IValidator;
            
            if (validator == null)
                continue;

            var validationContext = new ValidationContext<object>(argument);
            var result = await validator.ValidateAsync(validationContext,context.HttpContext.RequestAborted);
            
            if (result.IsValid)
                continue;
            
            foreach(var group in result.Errors.GroupBy(x => x.PropertyName))
                errors.Add(group.Key, group.Select(x => x.ErrorMessage).ToArray());
            if(errors.Count > 0)
            {
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "One or more validation errors occurred."
                });
                return;
            }
        } 
        await next();
    }
}