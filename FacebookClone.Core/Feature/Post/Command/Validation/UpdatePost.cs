using FacebookClone.Core.Feature.Post.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Post.Command.Validation
{
    public class UpdatePost : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePost()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.PostId).GreaterThan(0).WithMessage("PostId must be greater than 0");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Post content must not be empty")
                .MaximumLength(500).WithMessage("Post content must not exceed 500 characters");
            RuleFor(x => x.Privacy).IsInEnum().WithMessage("Invalid privacy setting");
        }
    }
}
