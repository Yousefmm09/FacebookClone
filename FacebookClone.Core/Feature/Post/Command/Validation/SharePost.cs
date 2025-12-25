using FacebookClone.Core.Feature.Post.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Command.Validation
{
    public class SharePost : AbstractValidator<SharePostCommand>
    {
        public SharePost()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.postId).GreaterThan(0).WithMessage("PostId must be greater than 0");
        }
    }
}
