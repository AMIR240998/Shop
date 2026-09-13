using FluentValidation;
using ShopApi.Contracts.Address;

namespace ShopApi.Validators.Address;

public class CreateAddressValidation : AbstractValidator<CreateAddressRequest>
{
    public CreateAddressValidation()
    {
        RuleFor(a => a.UserId).NotEmpty().NotNull().WithMessage("CustomerId is required");
        RuleFor(a => a.Province).NotEmpty().NotNull().WithMessage("Province is required");
        RuleFor(a => a.City).NotEmpty().NotNull().WithMessage("City is required");
        RuleFor(a => a.AddressText).NotEmpty().NotNull().WithMessage("Address is required");
        RuleFor(a => a.PostalCode).NotEmpty().NotNull().WithMessage("PostalCode is required");
    }
}