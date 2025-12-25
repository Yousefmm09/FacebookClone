using FacebookClone.Core.Feature.Post.Queries.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Queries.Validation
{
    public class GetPostCursor : AbstractValidator<GetPostsCursorQuery>
    {
        public GetPostCursor()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.LastId).NotEmpty().WithMessage("Cursor must not be empty");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}
