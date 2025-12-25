using FacebookClone.Core.Feature.Post.Queries.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Queries.Validation
{
    public class GetPostById : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostById()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.PostId).GreaterThan(0).WithMessage("PostId must be greater than 0");
        }
    }
}
