using FacebookClone.Core.Feature.Post.Queries.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Queries.Validation
{
    public class GetPostPaged : AbstractValidator<GetPostsPagedQuery>
    {
        public GetPostPaged()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}
