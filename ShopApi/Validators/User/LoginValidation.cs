using FluentValidation;
using ShopApi.Contracts.Login;

namespace ShopApi.Validators.Customer;

public class LoginValidation : AbstractValidator<LoginRequest>
{
    public LoginValidation()
    {
        RuleFor(f => f.Username).NotEmpty().Null().WithMessage("Username is required.")
            .MaximumLength(40).WithMessage("Username cannot exceed 40 characters.");
        RuleFor(f => f.PasswordHash).NotEmpty().Null().WithMessage("PasswordHash is required.")
            .MaximumLength(40).WithMessage("PasswordHash cannot exceed 40 characters.");
    }
}