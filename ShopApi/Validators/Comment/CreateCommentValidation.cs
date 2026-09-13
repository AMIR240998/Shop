using FluentValidation;
using ShopApi.Contracts.Comment;

namespace ShopApi.Validators.Comment;

public class CreateCommentValidation : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentValidation()
    {
        RuleFor(c => c.UserId).NotEmpty().NotNull().WithMessage("CustomerId is required");
        RuleFor(c => c.ProductId).NotEmpty().NotNull().WithMessage("ProductId is required");
        RuleFor(c => c.Text).NotNull().WithMessage("Text is required")
            .MaximumLength(200).WithMessage("The text length must be less than 200 characters.");
        RuleFor(c => c.Rate).NotNull().WithMessage("Rate is required")
            .InclusiveBetween((short)1, (short)5).WithMessage("The score must be between 1 and 5.");
    }
}