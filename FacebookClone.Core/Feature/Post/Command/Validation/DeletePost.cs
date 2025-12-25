using FacebookClone.Core.Feature.Post.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Command.Validation
{
    public class DeletePost : AbstractValidator<DeletePostCommand>
    {
        public DeletePost()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.PostId).GreaterThan(0).WithMessage("PostId must be greater than 0");

        }
    }

}
