using FluentValidation;
using ShopApi.Contracts.Products;

namespace ShopApi.Validators.Product;

public class UpdateProductValidation : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidation()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Product name must be specified")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Product description must be specified")
            .MaximumLength(1000).WithMessage("Product description must not exceed 1000 characters");
        RuleFor(p => p.Price).NotEmpty().NotNull().WithMessage("Product is Required")
            .GreaterThan(0).WithMessage("Product Price must be greater than 0");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Product Price must be greater than 0");
        RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Image url must be specified");
        RuleFor(p => p.CategoryId).NotNull().NotEmpty().WithMessage("CategoryId must be specified");
    }
}