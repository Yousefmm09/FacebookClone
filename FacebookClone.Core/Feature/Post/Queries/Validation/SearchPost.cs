using FacebookClone.Core.Feature.Post.Queries.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Queries.Validation
{
    public class SearchPost : AbstractValidator<SearchPostsQuery>
    {
        public SearchPost()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Search text must not be empty");
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}
