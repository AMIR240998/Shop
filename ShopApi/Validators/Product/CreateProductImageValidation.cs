using FluentValidation;
using ShopApi.Contracts.ProductImage;
using ShopApi.Contracts.ProductImages;

namespace ShopApi.Validators.Product;

public class CreateProductImageValidation : AbstractValidator<CreateProductImageRequest>
{
    public CreateProductImageValidation()
    {
        RuleFor(i => i.ProductId).GreaterThan(0).WithMessage("Product id must be greater than 0")
            .NotNull().NotEmpty().WithMessage("Product id must not be empty");
        RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image url must be empty");
    }
}