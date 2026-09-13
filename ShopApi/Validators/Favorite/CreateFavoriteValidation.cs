using FluentValidation;
using ShopApi.Contracts.Favorite;

namespace ShopApi.Validators.Favorite;

public class CreateFavoriteValidation : AbstractValidator<CreateFavoriteRequest>
{
    public CreateFavoriteValidation()
    {
        RuleFor(f => f.UserId).NotNull().WithMessage("CustomerId is required.");
        RuleFor(f => f.ProductId).NotNull().WithMessage("ProductId is required.");
    }
}