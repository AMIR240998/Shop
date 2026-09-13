using FluentValidation;
using ShopApi.Contracts.Order;
using ShopApi.Contracts.Orders;

namespace ShopApi.Validators.Order;

public class CreateOrderItemValidation : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemValidation()
    {
        RuleFor(o => o.ProductId).NotNull().NotEmpty().WithMessage("Product ID is required.");
        RuleFor(o => o.Count).NotNull().NotEmpty().WithMessage("Count is required.");
    }
}