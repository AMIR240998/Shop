using FluentValidation;
using ShopApi.Contracts.Customers;
using ShopApi.Contracts.User;

namespace ShopApi.Validators;

public class RegisterUserValidation : AbstractValidator<CreateUserRequest>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(400).WithMessage("Email cannot exceed 400 characters")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(400).WithMessage("Address cannot exceed 400 characters");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(40).WithMessage("Username cannot exceed 40 characters");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(40).WithMessage("Password cannot exceed 40 characters");
    }
}