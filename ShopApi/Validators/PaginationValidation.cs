using FluentValidation;
using ShopApi.Contracts.Pagination;

namespace ShopApi.Validators;

public class PaginationValidation : AbstractValidator<PaginationRequest>
{
    public PaginationValidation()
    {
        RuleFor(request => request.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than zero");
    }
}